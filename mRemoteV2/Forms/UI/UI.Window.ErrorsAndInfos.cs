using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AxWFICALib;
using Microsoft.VisualBasic;
using mRemoteNC.App;
using My;
using WeifenLuo.WinFormsUI.Docking;

//using mRemoteNC.Runtime;

namespace mRemoteNC
{
    namespace UI
    {
        namespace Window
        {
            public class ErrorsAndInfos : Base
            {
                #region Form Init

                internal System.Windows.Forms.PictureBox pbError;
                internal System.Windows.Forms.Label lblMsgDate;
                internal System.Windows.Forms.ListView lvErrorCollector;
                internal System.Windows.Forms.ColumnHeader clmMessage;
                internal System.Windows.Forms.TextBox txtMsgText;
                internal System.Windows.Forms.ImageList imgListMC;
                private System.ComponentModel.Container components = null;
                internal System.Windows.Forms.ContextMenuStrip cMenMC;
                internal System.Windows.Forms.ToolStripMenuItem cMenMCCopy;
                internal System.Windows.Forms.ToolStripMenuItem cMenMCDelete;
                internal System.Windows.Forms.Panel pnlErrorMsg;

                private void InitializeComponent()
                {
                    this.components = new System.ComponentModel.Container();
                    this.Load += new System.EventHandler(this.ErrorsAndInfos_Load);
                    this.Resize += new System.EventHandler(this.ErrorsAndInfos_Resize);
                    this.pnlErrorMsg = new System.Windows.Forms.Panel();
                    this.txtMsgText = new System.Windows.Forms.TextBox();
                    this.txtMsgText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MC_KeyDown);
                    this.lblMsgDate = new System.Windows.Forms.Label();
                    this.pbError = new System.Windows.Forms.PictureBox();
                    this.lvErrorCollector = new System.Windows.Forms.ListView();
                    this.lvErrorCollector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MC_KeyDown);
                    this.lvErrorCollector.SelectedIndexChanged +=
                        new System.EventHandler(this.lvErrorCollector_SelectedIndexChanged);
                    this.clmMessage = new System.Windows.Forms.ColumnHeader();
                    this.cMenMC = new System.Windows.Forms.ContextMenuStrip(this.components);
                    this.cMenMCCopy = new System.Windows.Forms.ToolStripMenuItem();
                    this.cMenMCCopy.Click += new System.EventHandler(this.cMenMCCopy_Click);
                    this.cMenMCDelete = new System.Windows.Forms.ToolStripMenuItem();
                    this.cMenMCDelete.Click += new System.EventHandler(this.cMenMCDelete_Click);
                    this.imgListMC = new System.Windows.Forms.ImageList(this.components);
                    this.pnlErrorMsg.SuspendLayout();
                    ((System.ComponentModel.ISupportInitialize)this.pbError).BeginInit();
                    this.cMenMC.SuspendLayout();
                    this.SuspendLayout();
                    //
                    //pnlErrorMsg
                    //
                    this.pnlErrorMsg.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) |
                         System.Windows.Forms.AnchorStyles.Right);
                    this.pnlErrorMsg.BackColor = System.Drawing.SystemColors.Control;
                    this.pnlErrorMsg.Controls.Add(this.txtMsgText);
                    this.pnlErrorMsg.Controls.Add(this.lblMsgDate);
                    this.pnlErrorMsg.Controls.Add(this.pbError);
                    this.pnlErrorMsg.Location = new System.Drawing.Point(0, 1);
                    this.pnlErrorMsg.Name = "pnlErrorMsg";
                    this.pnlErrorMsg.Size = new System.Drawing.Size(198, 232);
                    this.pnlErrorMsg.TabIndex = 20;
                    //
                    //txtMsgText
                    //
                    this.txtMsgText.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) |
                          System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
                    this.txtMsgText.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    this.txtMsgText.Location = new System.Drawing.Point(40, 20);
                    this.txtMsgText.Multiline = true;
                    this.txtMsgText.Name = "txtMsgText";
                    this.txtMsgText.ReadOnly = true;
                    this.txtMsgText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
                    this.txtMsgText.Size = new System.Drawing.Size(158, 211);
                    this.txtMsgText.TabIndex = 30;
                    //
                    //lblMsgDate
                    //
                    this.lblMsgDate.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) |
                         System.Windows.Forms.AnchorStyles.Right);
                    this.lblMsgDate.Font = new System.Drawing.Font("Tahoma", (float)(8.25F),
                                                                   System.Drawing.FontStyle.Italic,
                                                                   System.Drawing.GraphicsUnit.Point, (byte)(0));
                    this.lblMsgDate.Location = new System.Drawing.Point(40, 5);
                    this.lblMsgDate.Name = "lblMsgDate";
                    this.lblMsgDate.Size = new System.Drawing.Size(155, 13);
                    this.lblMsgDate.TabIndex = 40;
                    //
                    //pbError
                    //
                    this.pbError.InitialImage = null;
                    this.pbError.Location = new System.Drawing.Point(2, 5);
                    this.pbError.Name = "pbError";
                    this.pbError.Size = new System.Drawing.Size(32, 32);
                    this.pbError.TabIndex = 0;
                    this.pbError.TabStop = false;
                    //
                    //lvErrorCollector
                    //
                    this.lvErrorCollector.Anchor =
                        (System.Windows.Forms.AnchorStyles)
                        (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) |
                          System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
                    this.lvErrorCollector.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    this.lvErrorCollector.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.clmMessage });
                    this.lvErrorCollector.ContextMenuStrip = this.cMenMC;
                    this.lvErrorCollector.FullRowSelect = true;
                    this.lvErrorCollector.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
                    this.lvErrorCollector.HideSelection = false;
                    this.lvErrorCollector.Location = new System.Drawing.Point(203, 1);
                    this.lvErrorCollector.Name = "lvErrorCollector";
                    this.lvErrorCollector.ShowGroups = false;
                    this.lvErrorCollector.Size = new System.Drawing.Size(413, 232);
                    this.lvErrorCollector.SmallImageList = this.imgListMC;
                    this.lvErrorCollector.TabIndex = 10;
                    this.lvErrorCollector.UseCompatibleStateImageBehavior = false;
                    this.lvErrorCollector.View = System.Windows.Forms.View.Details;
                    //
                    //clmMessage
                    //
                    this.clmMessage.Text = Language.strColumnMessage;
                    this.clmMessage.Width = 184;
                    //
                    //cMenMC
                    //
                    this.cMenMC.Font = new System.Drawing.Font("Microsoft Sans Serif", (float)(8.25F),
                                                               System.Drawing.FontStyle.Regular,
                                                               System.Drawing.GraphicsUnit.Point, (byte)(0));
                    this.cMenMC.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.cMenMCCopy, this.cMenMCDelete });
                    this.cMenMC.Name = "cMenMC";
                    this.cMenMC.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
                    this.cMenMC.Size = new System.Drawing.Size(137, 48);
                    //
                    //cMenMCCopy
                    //
                    this.cMenMCCopy.Image = global::My.Resources.Resources.Copy;
                    this.cMenMCCopy.Name = "cMenMCCopy";
                    this.cMenMCCopy.ShortcutKeys =
                        (System.Windows.Forms.Keys)(System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C);
                    this.cMenMCCopy.Size = new System.Drawing.Size(136, 22);
                    this.cMenMCCopy.Text = Language.strMenuCopy;
                    //
                    //cMenMCDelete
                    //
                    this.cMenMCDelete.Image = global::My.Resources.Resources.Delete;
                    this.cMenMCDelete.Name = "cMenMCDelete";
                    this.cMenMCDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
                    this.cMenMCDelete.Size = new System.Drawing.Size(136, 22);
                    this.cMenMCDelete.Text = Language.strMenuDelete;
                    //
                    //imgListMC
                    //
                    this.imgListMC.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
                    this.imgListMC.ImageSize = new System.Drawing.Size(16, 16);
                    this.imgListMC.TransparentColor = System.Drawing.Color.Transparent;
                    //
                    //ErrorsAndInfos
                    //
                    this.ClientSize = new System.Drawing.Size(617, 233);
                    this.Controls.Add(this.lvErrorCollector);
                    this.Controls.Add(this.pnlErrorMsg);
                    this.HideOnClose = true;
                    this.Icon = global::My.Resources.Resources.Info_Icon;
                    this.Name = "ErrorsAndInfos";
                    this.TabText = Language.strMenuNotifications;
                    this.Text = Language.strMenuNotifications;
                    this.pnlErrorMsg.ResumeLayout(false);
                    this.pnlErrorMsg.PerformLayout();
                    ((System.ComponentModel.ISupportInitialize)this.pbError).EndInit();
                    this.cMenMC.ResumeLayout(false);
                    this.ResumeLayout(false);
                }

                #endregion Form Init

                #region Public Properties

                private DockContent _PreviousActiveForm;

                public DockContent PreviousActiveForm
                {
                    get { return this._PreviousActiveForm; }
                    set { this._PreviousActiveForm = value; }
                }

                #endregion Public Properties

                #region Form Stuff

                private void ErrorsAndInfos_Load(object sender, System.EventArgs e)
                {
                    ApplyLanguage();
                }

                private void ApplyLanguage()
                {
                    clmMessage.Text = Language.strColumnMessage;
                    cMenMCCopy.Text = Language.strMenuCopy;
                    cMenMCDelete.Text = Language.strMenuDelete;
                    TabText = Language.strMenuNotifications;
                    Text = Language.strMenuNotifications;
                }

                #endregion Form Stuff

                #region Public Methods

                public ErrorsAndInfos(DockContent Panel)
                {
                    this.WindowType = Type.ErrorsAndInfos;
                    this.DockPnl = Panel;
                    this.InitializeComponent();
                    this.LayoutVertical();
                    this.FillImageList();
                }

                #endregion Public Methods

                #region Private Methods

                private void FillImageList()
                {
                    this.imgListMC.Images.Add(global::My.Resources.Resources.InformationSmall);
                    this.imgListMC.Images.Add(global::My.Resources.Resources.WarningSmall);
                    this.imgListMC.Images.Add(global::My.Resources.Resources.ErrorSmall);
                }

                private ControlLayout _Layout = ControlLayout.Vertical;

                private void LayoutVertical()
                {
                    try
                    {
                        this.pnlErrorMsg.Location = new Point(0, System.Convert.ToInt32(this.Height - 200));
                        this.pnlErrorMsg.Size = new Size(System.Convert.ToInt32(this.Width),
                                                         System.Convert.ToInt32(this.Height - this.pnlErrorMsg.Top));
                        this.pnlErrorMsg.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                        this.txtMsgText.Size = new Size(this.pnlErrorMsg.Width - this.pbError.Width - 8,
                                                        this.pnlErrorMsg.Height - 20);
                        this.lvErrorCollector.Location = new Point(0, 0);
                        this.lvErrorCollector.Size = new Size(System.Convert.ToInt32(this.Width),
                                                              System.Convert.ToInt32(this.Height -
                                                                                     this.pnlErrorMsg.Height - 5));
                        this.lvErrorCollector.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right |
                                                       AnchorStyles.Top;

                        this._Layout = ControlLayout.Vertical;
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("LayoutVertical (UI.Window.ErrorsAndInfos) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void LayoutHorizontal()
                {
                    try
                    {
                        this.pnlErrorMsg.Location = new Point(0, 0);
                        this.pnlErrorMsg.Size = new Size(200, System.Convert.ToInt32(this.Height));
                        this.pnlErrorMsg.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top;
                        this.txtMsgText.Size = new Size(this.pnlErrorMsg.Width - this.pbError.Width - 8,
                                                        this.pnlErrorMsg.Height - 20);
                        this.lvErrorCollector.Location = new Point(this.pnlErrorMsg.Width + 5, 0);
                        this.lvErrorCollector.Size =
                            new Size(System.Convert.ToInt32(this.Width - this.pnlErrorMsg.Width - 5),
                                     System.Convert.ToInt32(this.Height));
                        this.lvErrorCollector.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right |
                                                       AnchorStyles.Top;

                        this._Layout = ControlLayout.Horizontal;
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("LayoutHorizontal (UI.Window.ErrorsAndInfos) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void ErrorsAndInfos_Resize(object sender, System.EventArgs e)
                {
                    try
                    {
                        if (this.Width > this.Height)
                        {
                            if (this._Layout == ControlLayout.Vertical)
                            {
                                this.LayoutHorizontal();
                            }
                        }
                        else
                        {
                            if (this._Layout == ControlLayout.Horizontal)
                            {
                                this.LayoutVertical();
                            }
                        }

                        this.lvErrorCollector.Columns[0].Width = this.lvErrorCollector.Width - 20;
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("ErrorsAndInfos_Resize (UI.Window.ErrorsAndInfos) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void pnlErrorMsg_ResetDefaultStyle()
                {
                    try
                    {
                        this.pnlErrorMsg.BackColor = Color.FromKnownColor(KnownColor.Control);
                        this.pbError.Image = null;
                        this.txtMsgText.Text = "";
                        this.txtMsgText.BackColor = Color.FromKnownColor(KnownColor.Control);
                        this.lblMsgDate.Text = "";
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("pnlErrorMsg_ResetDefaultStyle (UI.Window.ErrorsAndInfos) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void MC_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
                {
                    try
                    {
                        if (e.KeyCode == Keys.Escape)
                        {
                            try
                            {
                                if (this._PreviousActiveForm != null)
                                {
                                    this._PreviousActiveForm.Show(frmMain.Default.pnlDock);
                                }
                                else
                                {
                                    Runtime.Windows.treeForm.Show(frmMain.Default.pnlDock);
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("MC_KeyDown (UI.Window.ErrorsAndInfos) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void lvErrorCollector_SelectedIndexChanged(System.Object sender, System.EventArgs e)
                {
                    try
                    {
                        if (this.lvErrorCollector.SelectedItems.Count == 0 ||
                            this.lvErrorCollector.SelectedItems.Count > 1)
                        {
                            this.pnlErrorMsg_ResetDefaultStyle();
                            return;
                        }

                        ListViewItem sItem = this.lvErrorCollector.SelectedItems[0];
                        Messages.Message eMsg = (Messages.Message)sItem.Tag;
                        switch (eMsg.MsgClass)
                        {
                            case Messages.MessageClass.InformationMsg:
                                this.pbError.Image = global::My.Resources.Resources.Information;
                                this.pnlErrorMsg.BackColor = Color.LightSteelBlue;
                                this.txtMsgText.BackColor = Color.LightSteelBlue;
                                break;
                            case Messages.MessageClass.WarningMsg:
                                this.pbError.Image = global::My.Resources.Resources.Warning;
                                this.pnlErrorMsg.BackColor = Color.Gold;
                                this.txtMsgText.BackColor = Color.Gold;
                                break;
                            case Messages.MessageClass.ErrorMsg:
                                this.pbError.Image = global::My.Resources.Resources._Error;
                                this.pnlErrorMsg.BackColor = Color.IndianRed;
                                this.txtMsgText.BackColor = Color.IndianRed;
                                break;
                        }

                        this.lblMsgDate.Text = eMsg.MsgDate.ToString();
                        this.txtMsgText.Text = eMsg.MsgText;
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("lvErrorCollector_SelectedIndexChanged (UI.Window.ErrorsAndInfos) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void cMenMCCopy_Click(System.Object sender, System.EventArgs e)
                {
                    this.CopyMessageToClipboard();
                }

                private void CopyMessageToClipboard()
                {
                    try
                    {
                        if (this.lvErrorCollector.SelectedItems.Count > 0)
                        {
                            string strCopyText;
                            strCopyText = "----------" + Constants.vbNewLine;

                            foreach (ListViewItem lvItem in this.lvErrorCollector.SelectedItems)
                            {
                                strCopyText += (lvItem.Tag as Messages.Message).MsgClass.ToString() +
                                               Constants.vbNewLine;
                                strCopyText += (lvItem.Tag as Messages.Message).MsgDate + Constants.vbNewLine;
                                strCopyText += (lvItem.Tag as Messages.Message).MsgText + Constants.vbNewLine;
                                strCopyText += "----------" + Constants.vbNewLine;
                            }

                            Clipboard.SetText(strCopyText);
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("CopyMessageToClipboard (UI.Window.ErrorsAndInfos) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                private void cMenMCDelete_Click(System.Object sender, System.EventArgs e)
                {
                    this.DeleteMessages();
                }

                private void DeleteMessages()
                {
                    try
                    {
                        if (this.lvErrorCollector.SelectedItems.Count > 0)
                        {
                            foreach (ListViewItem lvItem in this.lvErrorCollector.SelectedItems)
                            {
                                lvItem.Remove();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Runtime.MessageCollector.AddMessage(Messages.MessageClass.ErrorMsg,
                                                            (string)
                                                            ("DeleteMessages (UI.Window.ErrorsAndInfos) failed" +
                                                             Constants.vbNewLine + ex.Message), true);
                    }
                }

                #endregion Private Methods

                public enum ControlLayout
                {
                    Vertical = 0,
                    Horizontal = 1
                }
            }
        }
    }
}