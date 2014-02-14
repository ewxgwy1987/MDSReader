using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace MDSReader
{
    public class MDSReader
    {
        #region Class Field and Property
        private string m_filepath;
        private string m_colname;
        private int crr_rowidx = -1;
        private int crr_colidx = -1;
        private Application m_xlsapp;
        private Workbook m_xlswbk;
        private Worksheet m_worksheet;

        private int start_rowidx = 2;
        private int start_colidx = 2;
        private int end_rowidx = -1;
        private int end_colidx = -1;

        private string[] m_rowheads;
        private string[] m_colheads;

        public string[] RowHeads
        {
            get
            {
                return this.m_rowheads;
            }
        }

        public string[] ColHeads
        {
            get
            {
                return this.m_colheads;
            }
        }


        // The name of current class 
        private static readonly string _className =
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString();
        // Create a logger for use in this class
        private static readonly log4net.ILog _logger =
                    log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Constructor, Dispose, Finalize and Destructor
        public MDSReader(string fpath_inputxls)
        {
            string thisMethod = _className + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()";
            string errstr = "Class:[" + _className + "]" + "Method:<" + thisMethod + ">\n";
            int i;
            crr_rowidx = -1;
            end_rowidx = -1;
            end_colidx = -1;

            try
            {
                this.m_filepath = Path.GetFullPath(fpath_inputxls);
                //FileInfo fio = new FileInfo(this.m_filepath);
                m_xlsapp = new Application();
                //string apppath = m_xlsapp.StartupPath;
                //ReadOnly open
                m_xlswbk = m_xlsapp.Workbooks.Open(this.m_filepath,
                true, Missing.Value, Missing.Value, Missing.Value
                , Missing.Value, Missing.Value, Missing.Value, Missing.Value
                , Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                this.m_worksheet = (Worksheet)m_xlswbk.Sheets[1];

                // count rows in excel
                for (i = start_rowidx; ; i++)
                {
                    Range rowhead = m_worksheet.Cells[i, start_colidx - 1];
                    if (rowhead.Value == null || rowhead.Text.ToString() == "")
                        break;
                }
                this.end_rowidx = i - 1;
                crr_rowidx = start_rowidx;

                // count columns in excel
                for (i = start_colidx; ; i++)
                {
                    Range colhead = (Range)m_worksheet.Cells[start_rowidx - 1, i];
                    if (colhead.Value == null || colhead.Text.ToString() == "")
                        break;
                }
                this.end_colidx = i - 1;

                if (end_rowidx != -1 && end_colidx != -1)
                {
                    Array array_str;
                    char ch_first_colidx = (char)((int)'A' + start_colidx - 2);
                    char ch_start_colidx = (char)((int)'A' + start_colidx - 1);
                    char ch_end_colidx = (char)((int)'A' + end_colidx - 1);

                    Range rg_rowheads = m_worksheet.get_Range(ch_first_colidx + start_rowidx.ToString(), ch_first_colidx + end_rowidx.ToString());
                    array_str = (Array)rg_rowheads.Cells.Value;
                    this.m_rowheads = new string[array_str.Length];
                    i = 0;
                    foreach (object item in array_str)
                    {
                        this.m_rowheads[i] = (string)item;
                        i++;
                    }


                    Range rg_colheads = m_worksheet.get_Range(ch_start_colidx + (start_rowidx - 1).ToString(), ch_end_colidx + (start_rowidx - 1).ToString());
                    array_str = (Array)rg_colheads.Cells.Value;
                    this.m_colheads = new string[array_str.Length];
                    i = 0;
                    foreach (object item in array_str)
                    {
                        this.m_colheads[i] = (string)item;
                        i++;
                    }
                }
            }
            catch (Exception exp)
            {
                errstr += exp.ToString();
                _logger.Error(errstr);
            }
        }

        ~MDSReader()
        {
            Dispose();
        }

        #endregion

        #region Member Function
        public void Dispose()
        {
            string thisMethod = _className + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()";
            string errstr = "Class:[" + _className + "]" + "Method:<" + thisMethod + ">\n";

            try
            {
                if (this.m_xlswbk != null)
                {
                    this.m_xlswbk.Close();

                    this.m_xlswbk = null;
                }

                if (this.m_xlsapp != null)
                {
                    this.m_xlsapp.Quit();
                    this.m_xlsapp = null;
                }
                _logger.Info("MESReader Quit!");
            }
            catch (Exception exp)
            {
                errstr += exp.ToString();
                _logger.Error(errstr);
            }
        }

        public bool SetWorkColumn(string colname)
        {
            string thisMethod = _className + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()";
            string errstr = "Class:[" + _className + "]" + "Method:<" + thisMethod + ">\n";
            int i;
            crr_colidx = -1;
            crr_rowidx = start_rowidx;

            try
            {
                if (this.m_worksheet == null)
                {
                    errstr += "No WorkSheet can be found";
                    _logger.Error(errstr);
                    return false;
                }

                // get the current column index by column name
                for (i = start_colidx; i <= end_colidx; i++)
                {
                    Range colhead = (Range)m_worksheet.Cells[start_rowidx - 1, i];
                    if (colhead.Value == null || colhead.Text.ToString() == "")
                        break;
                    else if (colhead.Text.ToString() == colname)
                        crr_colidx = i;
                }
                this.m_colname = colname;
                return true;
            }
            catch (Exception exp)
            {
                errstr += exp.ToString();
                _logger.Error(errstr);
                return false;
            }
        }

        public string NextTagValue()
        {
            string thisMethod = _className + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()";
            string errstr = "Class:[" + _className + "]" + "Method:<" + thisMethod + ">\n";

            string strval = null;

            try
            {
                if (this.crr_rowidx != -1 && this.crr_colidx != -1
                    && this.crr_rowidx <= this.end_rowidx && this.crr_colidx <= this.end_colidx)
                {

                    Range crr_cell = this.m_worksheet.Cells[crr_rowidx, crr_colidx];
                    if (crr_cell.Value != null)
                        strval = crr_cell.Text.ToString().Trim();
                    this.crr_rowidx++;

                }
            }
            catch (Exception exp)
            {
                errstr += exp.ToString();
                _logger.Error(errstr);

            }

            return strval;
        }

        public string GetTagValue(string colname, int rownum)
        {
            string thisMethod = _className + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + "()";
            string errstr = "Class:[" + _className + "]" + "Method:<" + thisMethod + ">\n";

            string strval = null;
            int i;
            int tmp_colnum = -1;
            int tmp_rownum = -1;

            try
            {
                for (i = 0; i < this.m_colheads.Length; i++)
                {
                    if (this.m_colheads[i].Equals(colname))
                    {
                        tmp_colnum = start_colidx + i;
                        break;
                    }
                }
                tmp_rownum = start_rowidx + rownum - 1;

                if (tmp_colnum != -1 && tmp_rownum != -1
                    && tmp_rownum <= this.end_rowidx && tmp_colnum <= this.end_colidx)
                {

                    Range crr_cell = this.m_worksheet.Cells[tmp_rownum, tmp_colnum];
                    if (crr_cell.Value != null)
                        strval = crr_cell.Text.ToString().Trim();
                    this.crr_rowidx++;

                }
            }
            catch (Exception exp)
            {
                errstr += exp.ToString();
                _logger.Error(errstr);

            }

            return strval;

        }
        #endregion
    }
}
