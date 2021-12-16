using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace LibreriaClases
{
    public static class ExportExcel
    {
        #region EXPORT TABLE FUNCTION
        // EXPORTING EXCEL FUNCTION
            //Functions that enables exporting to excel by setting table.ExportToExcel
            // it is needed to have installed the microsoft excel
        public static void ExportToExcel(this DataTable tbl)
        {
            try
            {
                // new excel
                var new_excel = new Excel.Application();
                new_excel.Workbooks.Add();

                Excel._Worksheet workSheet = new_excel.ActiveSheet;

                // loop for writing all the cells
                for (int i = 0; i < tbl.Columns.Count; i++)
                {
                    workSheet.Cells[1, i + 1] = tbl.Columns[i].ColumnName;
                }

                for (int i = 0; i < tbl.Rows.Count; i++)
                {

                    for (int j = 0; j < tbl.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 2, j + 1] = Convert.ToDouble(tbl.Rows[i][j]);
                    }
                }
                new_excel.Visible = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion EXPORT TABLE FUNCTION

    }
}
