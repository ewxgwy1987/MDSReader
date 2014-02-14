using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.IO;
using System.Xml;
using PALS;

namespace MDSReader
{
    public partial class Form1 : Form
    {
        private MDSReader mdsreader;
        private string path_TestFile;
        private int test_rate;
        private const int margin = 2;
        private const int padding = 0;
        private const int unit_length = 250;
        private const int unit_height = 30;
        private const int unit_lbbit_l = 100;
        private const int unit_lbbit_h = 22;
        private const int unit_btnbit_l = 100;
        private const int unit_btnbit_h = 22;

        // The name of current class 
        private static readonly string _className =
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString();
        // Create a logger for use in this class
        private static readonly log4net.ILog _logger =
                    log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string Path_XMLFileSetting = @"../../cfg/CFG_MDSReader.xml";
            tbxFilePah.Text = @"../../InputFiles/testfile.xlsx";
            FileInfo FI_Setting = new FileInfo(Path_XMLFileSetting);
            XmlElement xmlRoot = PALS.Utilities.XMLConfig.GetConfigFileRootElement(ref FI_Setting);
            XmlElement log4netConfig = (XmlElement)PALS.Utilities.XMLConfig.GetConfigSetElement(ref xmlRoot, "log4net");
            log4net.Config.XmlConfigurator.Configure(log4netConfig);

            tbxTestRate.Text = "1000";
        }

        private void RefreshTestArea()
        {
            string thisMethod = _className + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()";
            string errstr = "Class:[" + _className + "]" + "Method:<" + thisMethod + ">\n";
            Point Tpnl_base = TestPanel.Location;
            int sx = margin;
            int sy = margin;
            foreach (Control ctl in TestPanel.Controls)
            {
                TestPanel.Controls.Remove(ctl);
            }

            try
            {
                
                mdsreader = new MDSReader(path_TestFile);

                string[] testbits = mdsreader.ColHeads;
                foreach (string bitname in testbits)
                {
                    Panel pnl_unit = new Panel();
                    pnl_unit.Margin = new Padding(margin);
                    pnl_unit.Padding = new Padding(margin);
                    pnl_unit.Size = new Size(unit_length, unit_height);
                    pnl_unit.TabIndex = 2;
                    pnl_unit.Location = new Point(sx, sy);
                    //pnl_unit.BorderStyle = BorderStyle.FixedSingle;
                    sy += unit_height + margin;

                    Label unit_lbbit = new Label();
                    unit_lbbit.Name = "lb_" + bitname;
                    unit_lbbit.Size = new Size(unit_lbbit_l, unit_lbbit_h);
                    unit_lbbit.Text = bitname;
                    unit_lbbit.Margin = new Padding(margin);
                    unit_lbbit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    //unit_lbbit.BorderStyle = BorderStyle.FixedSingle;
                    unit_lbbit.Location = new Point(margin,margin);
                    unit_lbbit.TabIndex = 3;
                    pnl_unit.Controls.Add(unit_lbbit);

                    Button unit_btnbit = new Button();
                    unit_btnbit.Name = "btn_" + bitname;
                    unit_btnbit.Size = new Size(unit_btnbit_l, unit_btnbit_h);
                    unit_btnbit.Margin = new Padding(margin);
                    unit_btnbit.Text = "Test " + bitname;
                    unit_btnbit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    unit_btnbit.Click += new System.EventHandler(btTestBit_Click);
                    unit_btnbit.Location = new Point(2 * margin + unit_lbbit_l, margin);
                    unit_btnbit.TabIndex = 3;
                    pnl_unit.Controls.Add(unit_btnbit);
                    
                    
                    this.TestPanel.Controls.Add(pnl_unit);
                }
                this.TestPanel.ResumeLayout(false);
                this.TestPanel.PerformLayout();
                this.ResumeLayout(false);
                this.PerformLayout();
                this.Refresh();
            }
            catch (Exception exp)
            {
                errstr += exp.ToString();
                _logger.Error(errstr);
            }
        }

        private void btTestBit_Click(object sender, EventArgs e)
        {
            Button unit_btntag = sender as Button;
            int idx = (unit_btntag.Name).IndexOf("_");
            string colname = (unit_btntag.Name).Substring(idx + 1);

            Thread thrd_mdstest = new Thread(ThrdFun_MDSTest);
            thrd_mdstest.IsBackground = true;
            thrd_mdstest.Start(colname);
        }

        
        private void ThrdFun_MDSTest(object obj_colname)
        {
            string colname = (string)obj_colname;

            // Clear all bit status to be 0;
            mdsreader.SetWorkColumn(colname);
            string tagval;
            do
            {
                tagval = mdsreader.NextTagValue();
                if (tagval == null)
                    break;
                WriteToOPCServer(tagval, 0);
            } while (tagval != null);

            //Test MDS tag from 0 to 1
            mdsreader.SetWorkColumn(colname);
            do
            {
                tagval = mdsreader.NextTagValue();
                if (tagval == null)
                    break;
                WriteToOPCServer(tagval, 1);
                Thread.Sleep(this.test_rate);
            } while (tagval != null);
            
        }

        public delegate void DlgtFun_WriteTestLog(string testData);
        private void WriteTestLog(string testData)
        {
            if (this.outTxtBox.InvokeRequired)
            {
                DlgtFun_WriteTestLog deleg = new DlgtFun_WriteTestLog(WriteTestLog);

                this.Invoke(deleg, testData);
            }
            else
            {
                this.outTxtBox.Text = testData + "\r\n" + this.outTxtBox.Text;
            }
        }

        private void Form1_FormClosed(Object sender, FormClosedEventArgs e)
        {
            if (mdsreader != null)
            {
                mdsreader.Dispose();
                mdsreader = null;
            }
        }

        private void fpathSubmitBtn_Click(object sender, EventArgs e)
        {
            this.path_TestFile = this.tbxFilePah.Text;
            this.test_rate = int.Parse(tbxTestRate.Text);
            RefreshTestArea();
        }

        private void tbxTestRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
            this.tbxFilePah.Text = this.openFileDialog1.FileName;
        }

        private bool WriteToOPCServer(string tagname, int val)
        {
            string output = "Write " + tagname + " with Value:" + val.ToString() + " to OPC server.";
            

            #region write to OPC server

            //The function of writing data to OPC added here

            #endregion

            WriteTestLog(output);
            return true;
        }
     
    }
}
