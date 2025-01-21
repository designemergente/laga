using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace Laga.IO
{
    internal static class IOExcel
    {
        public static bool TestExcelOpen(string dir, out Excel.Workbook xlBook)
        {
            string wb = IODataFolder.GetFileName(dir);
            bool isOpen = true;
            Excel.Application excelApp;

            try
            {
                //getting the current running instance of an excel application
                excelApp = (Excel.Application)Marshal.GetActiveObject("Excel.Application");
                xlBook = excelApp.Workbooks.get_Item(wb);
            }
            catch
            {
                isOpen = false;
                xlBook = null;
            }

            return isOpen;
        }

        public static string ExcelFileName(string dir)
        {

            return dir;
        }
    }
}
