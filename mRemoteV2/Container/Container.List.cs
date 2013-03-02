using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using AxWFICALib;
using Microsoft.VisualBasic;

namespace mRemoteNC
{
    namespace Container
    {
        public class List : CollectionBase
        {
            #region Public Properties

            public Container.Info this[object Index]
            {
                get
                {
                    if (Index is Container.Info)
                    {
                        return (Container.Info)Index;
                    }
                    else
                    {
                        return ((Container.Info)(List[System.Convert.ToInt32(Index)]));
                    }
                }
            }

            public new int Count
            {
                get { return List.Count; }
            }

            #endregion Public Properties

            #region Public Methods

            public Container.Info Add(Container.Info cInfo)
            {
                this.List.Add(cInfo);
                return cInfo;
            }

            public void AddRange(Container.Info[] cInfo)
            {
                foreach (Container.Info cI in cInfo)
                {
                    List.Add(cI);
                }
            }

            public Container.Info FindByConstantID(string id)
            {
                foreach (Container.Info contI in List)
                {
                    if (contI.ConnectionInfo.ConstantID == id)
                    {
                        return contI;
                    }
                }

                return null;
            }

            public Container.List Copy()
            {
                try
                {
                    return (Container.List)this.MemberwiseClone();
                }
                catch (Exception)
                {
                }

                return null;
            }

            public new void Clear()
            {
                this.List.Clear();
            }

            #endregion Public Methods
        }
    }
}