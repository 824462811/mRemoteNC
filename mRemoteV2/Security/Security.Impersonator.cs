using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.VisualBasic;
using mRemoteNC.App;

//using mRemoteNC.Runtime;

[assembly: SecurityPermissionAttribute(SecurityAction.RequestMinimum, UnmanagedCode = true)]
[assembly: PermissionSetAttribute(SecurityAction.RequestMinimum, Name = "FullTrust")]

namespace mRemoteNC
{
    namespace Security
    {
        public class Impersonator
        {
            #region Logon API

            private const int LOGON32_PROVIDER_DEFAULT = 0;

            private const int LOGON32_LOGON_INTERACTIVE = 2;
            // This parameter causes LogonUser to create a primary token.

            private const int SecurityImpersonation = 2;

            /*[DllImport("advapi32.dll", ExactSpelling=true, CharSet=CharSet.Auto, SetLastError=true)]
			private static extern int LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
*/

            [DllImport("advapi32.dll", SetLastError = true)]
            private static extern bool LogonUser(
                string lpszUsername,
                string lpszDomain,
                string lpszPassword,
                int dwLogonType,
                int dwLogonProvider,
                out IntPtr phToken
                );

            private const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x100;
            private const int FORMAT_MESSAGE_IGNORE_INSERTS = 0x200;
            private const int FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;

            [DllImport("kernel32.dll")]
            private static extern int FormatMessage(int dwFlags, ref IntPtr lpSource, int dwMessageId, int dwLanguageId,
                                                    ref string lpBuffer, int nSize, ref IntPtr Arguments);

            [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
            private static extern bool CloseHandle(IntPtr handle);

            [DllImport("advapi32.dll", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
            private static extern int DuplicateToken(IntPtr ExistingTokenHandle, int SECURITY_IMPERSONATION_LEVEL,
                                                     ref IntPtr DuplicateTokenHandle);

            #endregion Logon API

            private IntPtr tokenHandle = new IntPtr(0);
            private IntPtr dupeTokenHandle = new IntPtr(0);
            private WindowsImpersonationContext impersonatedUser = null;

            // GetErrorMessage formats and returns an error message corresponding to the input errorCode.
            private string GetErrorMessage(int errorCode)
            {
                int messageSize = 255;
                string lpMsgBuf = "";
                int dwFlags =
                    System.Convert.ToInt32(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM |
                                           FORMAT_MESSAGE_IGNORE_INSERTS);

                IntPtr ptrlpSource = IntPtr.Zero;
                IntPtr prtArguments = IntPtr.Zero;

                int retVal = FormatMessage(dwFlags, ref ptrlpSource, errorCode, 0, ref lpMsgBuf, messageSize,
                                           ref prtArguments);
                return lpMsgBuf.Trim(new char[] { char.Parse(Constants.vbCr), char.Parse(Constants.vbLf) });
            }

            [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
            public void StartImpersonation(string DomainName, string UserName, string Password)
            {
                try
                {
                    if (!(impersonatedUser == null))
                    {
                        throw (new Exception("Already impersonating a user."));
                    }

                    tokenHandle = IntPtr.Zero;
                    dupeTokenHandle = IntPtr.Zero;

                    int returnValue =
                        System.Convert.ToInt32(LogonUser(UserName, DomainName, Password,
                                                         System.Convert.ToInt32(LOGON32_LOGON_INTERACTIVE),
                                                         System.Convert.ToInt32(LOGON32_PROVIDER_DEFAULT),
                                                         out tokenHandle));

                    if (0 == returnValue)
                    {
                        int errCode = Marshal.GetLastWin32Error();
                        string errMsg = "LogonUser failed with error code: " + errCode.ToString() + "(" +
                                        GetErrorMessage(errCode) + ")";
                        ApplicationException exLogon = new ApplicationException(errMsg);
                        throw (exLogon);
                    }

                    returnValue =
                        System.Convert.ToInt32(DuplicateToken(tokenHandle, System.Convert.ToInt32(SecurityImpersonation),
                                                              ref dupeTokenHandle));
                    if (0 == returnValue)
                    {
                        CloseHandle(tokenHandle);
                        throw (new ApplicationException("Error trying to duplicate handle."));
                    }

                    // The token that is passed to the following constructor must
                    // be a primary token in order to use it for impersonation.
                    WindowsIdentity newId = new WindowsIdentity(dupeTokenHandle);
                    impersonatedUser = newId.Impersonate();
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                        (string)
                                                        ("Starting Impersonation failed (Sessions feature will not work)" +
                                                         Constants.vbNewLine + ex.Message), true);
                }
            }

            [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
            public void StopImpersonation()
            {
                if (impersonatedUser == null)
                {
                    return;
                }

                try
                {
                    impersonatedUser.Undo(); // Stop impersonating the user.
                }
                catch (Exception ex)
                {
                    Runtime.MessageCollector.AddMessage(Messages.MessageClass.WarningMsg,
                                                        (string)
                                                        ("Stopping Impersonation failed" + Constants.vbNewLine +
                                                         ex.Message), true);
                    throw;
                }
                finally
                {
                    if (dupeTokenHandle != IntPtr.Zero)
                    {
                        CloseHandle(tokenHandle);
                    }
                    if (dupeTokenHandle != IntPtr.Zero)
                    {
                        CloseHandle(dupeTokenHandle);
                    }

                    impersonatedUser = null;
                }
            }
        }
    }
}