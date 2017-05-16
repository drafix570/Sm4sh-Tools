﻿using SALT.PARAMS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parameters
{
    class ValuesWrapper : TreeNode
    {
        private static ContextMenuStrip _menu;
        public ValuesWrapper()
        {
            Params = new ParamList();
            labels = new List<string>();
            ContextMenuStrip = _menu;
        }
        static ValuesWrapper()
        {
            _menu = new ContextMenuStrip();
            _menu.Items.Add(new ToolStripMenuItem("Apply Labels..", null, ApplyLablesAction));
        }
        public ValuesWrapper(string text) : this() { Text = text; }

        public ParamList Params { get; set; }
        public List<string> labels { get; set; }

        private static void ApplyLablesAction(object sender, EventArgs e)
        {
            GetInstance<ValuesWrapper>().ApplyLabels();
        }
        public virtual void ApplyLabels()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (Directory.Exists(Path.Combine(Application.StartupPath, "templates")))
                    dlg.InitialDirectory = Path.Combine(Application.StartupPath, "templates");

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader reader = new StreamReader(dlg.FileName))
                    {
                        labels = reader.ReadToEnd().Split('\n').ToList();
                    }
                }
            }
        }

        protected static T GetInstance<T>() where T : ValuesWrapper { return FormProvider.Instance.treeView1.SelectedNode as T; }
    }
}
