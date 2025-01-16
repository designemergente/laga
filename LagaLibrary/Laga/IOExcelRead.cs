using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Office.Core;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

namespace Laga.IO
{
    /// <summary>
    /// Read excel files
    /// </summary>
    public class IOExcelRead
    {
        private readonly string filePath;
        private int sheetNum;
        private readonly string xlsxRange;
        private Excel.Application excelApp;
        private Excel.Workbook xlBook;
        private Excel.Worksheet xlSheet;
        private Excel.Range xlRange;
        private List<List<string>> dataExcel;

        #region public properties
        /// <summary>
        /// get the data by list format, use this to get the data from excel
        /// </summary>
        public List<List<string>> MatrixDataExcel
        {
            get
            {
                return dataExcel;
            }
        }
        /// <summary>
        /// Get Excel sheet number.
        /// </summary>
        public int SheetNum
        {
            get
            {
                return sheetNum;
            }
        }
        #endregion

        #region constructors
        /// <summary>
        /// The simpliest constructor
        /// </summary>
        /// <param name="FilePath">string, the path to the file</param>
        public IOExcelRead(string FilePath)
        {
            this.filePath = FilePath;
        }
        
        /// <summary>
        /// Constructor for IOExcel most flexible constructor.
        /// therfore you have to deal with all operations:
        /// open, read or write and close.
        /// </summary>
        /// <param name="FilePath">String, the Path to the file</param>
        /// <param name="SheetNumber">Integer, the index sheet in the excel book</param>
        public IOExcelRead(string FilePath, int SheetNumber)
        {
            this.filePath = FilePath;
            this.sheetNum = SheetNumber;
        }
        /// <summary>
        /// Simple constructor to read data straightforward from excel
        /// the simpliest and faster option
        /// </summary>
        /// <param name="FilePath">string, the Path to the file</param>
        /// <param name="SheetNumber">integer, the index sheet in the excel book</param>
        /// <param name="XlsxRange">string, the excel range to read the data</param>
        public IOExcelRead(string FilePath, int SheetNumber, string XlsxRange)
        {
            this.filePath = FilePath;
            this.sheetNum = SheetNumber;
            this.xlsxRange = XlsxRange;
            IORead_OpenExcelApp(false);
            IOReadRange(xlsxRange);
            CloseExcelApp(false);
        }
        #endregion

        #region methods to read excel

        /// <summary>
        /// Read an specific cell looking for an excel shape
        /// </summary>
        /// <param name="cellAddress">the cell adress for the shape location</param>
        /// <returns>Excel.Shape</returns>
        public Excel.Shape IOReadShapeCell(string cellAddress)
        {
            try
            {
                string shapeAdress;
                foreach (Excel.Shape shape in xlSheet.Shapes)
                {
                    //shapeAdress = IOTextData.RemoveByCharacter((string)shape.TopLeftCell.Address, "$");
                    shapeAdress = IOTextData.RemoveByCharacter((string)shape.get_AlternativeText(),"$"); //not sure...
                    if (shapeAdress == cellAddress)
                    {

                        return shape;
                    }                
                }
                return null;
            }
            catch 
            {
                this.CloseExcelApp();
                return null; 
            }
        }

        /// <summary>
        /// read the cells range specified in the parameter.
        /// </summary>
        /// <param name="strXlRange">the range to read the excel, format "A1:B2" if is empty ("") will return the whole data in the workbook</param>
        public List<List<string>> IOReadRange(string strXlRange)
        {
            xlRange = (strXlRange == "") ? xlSheet.UsedRange : xlSheet.Range[strXlRange];
            dataExcel = new List<List<string>>();
            object[,] cellValues = (object[,])xlRange.Value2;
            List<string> lst;
            string str;
            for (int i = 1; i <= cellValues.GetLength(0); i++)
            {
                lst = new List<string>();
                for (int j = 1; j <= cellValues.GetLength(1); j++)
                {
                    str = (cellValues[i, j] != null) ? cellValues[i, j].ToString() : "!=";
                    lst.Add(str);
                }
                dataExcel.Add(lst);
            }
            return dataExcel;
        }
        /// <summary>
        /// Read a specific excel cell.
        /// </summary>
        /// <param name="cellAddress">The cell to read in excel, format "A1"</param>
        public string IOReadCell(string cellAddress)
        {
            xlRange = xlSheet.Range[cellAddress];
            object obDta = (object)xlRange.Value2;
            string str = (obDta != null) ? obDta.ToString() : "!=";
            return str;
        }
        #endregion

        #region open and close excel application
        /// <summary>
        /// Activates a specific Excel sheet to read.
        /// </summary>
        /// <param name="pos">The position of the excel sheet in the document.</param>
        /// <param name="display">decide to visualize the excel sheet.</param>
        public void IORead_SetActiveSheet(int pos, bool display)
        {
            //Excel.Sheets excelSheets = xlBook.Worksheets;
            Excel.Sheets excelSheets = xlBook.get_Worksheets();
            xlSheet = excelSheets.Item[pos];
            xlSheet.Activate();
            if (display)
                xlSheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
        }
        /// <summary>
        /// Open the App
        /// return all the excel sheet names.
        /// Close the App, release the Marshalls.
        /// </summary>
        public List<string> IORead_ExcelWorksheetNames()
        {
            excelApp = new Excel.Application();
            //xlBook = excelApp.Workbooks.Open(filePath);
            xlBook = excelApp.get_Workbooks().Open(filePath);

            List<string> lstExcelNames = new List<string>();
            //foreach (Excel.Worksheet ws in xlBook.Worksheets)
            
            foreach(Excel.Worksheet ws in xlBook.get_Worksheets())
            {
                lstExcelNames.Add(ws.Name);
            }
            CloseExcelApp(false);
            return lstExcelNames; //check the sheetnumber...
        }
        /// <summary>
        /// Open excel app based on the constructor.
        /// </summary>
        public void IORead_OpenExcelApp()
        {
            excelApp = new Excel.Application();
            //xlBook = excelApp.Workbooks.Open(filePath);
            //sheetNum = excelApp.Worksheets.Count;

            xlBook = excelApp.get_Workbooks().Open(filePath);
            sheetNum = excelApp.get_Worksheets().Count;
        }
        /// <summary>
        /// Open excel app based on the constructor.
        /// </summary>
        /// <param name="display">if is true, show the excel"</param>
        public void IORead_OpenExcelApp(bool display)
        {
            IORead_OpenExcelApp();
            if (display)
            {
                //excelApp.Visible = display;
                //excelApp.WindowState = Excel.XlWindowState.xlMaximized;

                excelApp.set_Visible(display);
                excelApp.set_WindowState(Excel.XlWindowState.xlMaximized);
            }
            //int c = excelApp.Worksheets.Count; //check the sheetnumber...
            int c = excelApp.get_Worksheets().Count;

            if ((sheetNum > c) || (sheetNum < 1)) //means there is no excel sheet to read.
            {
                //System.Windows.Forms.MessageBox.Show("No excel Sheet found");
                CloseExcelApp();
            }
            else
            {
                //xlSheet = xlBook.Sheets[sheetNum];
                xlSheet = xlBook.get_Worksheets()[sheetNum];
            }
        }
        /// <summary>
        /// Clean all the marshalls and kill excel.
        /// </summary>
        public void CloseExcelApp()
        {
            //clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if(xlRange != null)
            Marshal.ReleaseComObject(xlRange);  //kill the range..
            if(xlSheet != null)
            Marshal.ReleaseComObject(xlSheet);  //kill the sheet used
            excelApp.Quit();
            Marshal.ReleaseComObject(excelApp); //kill the excel
        }
        /// <summary>
        /// clean all the marshalls and close - save the excel.
        /// </summary>
        /// <param name="saveFile">if is true, will save the file</param>
        public void CloseExcelApp(bool saveFile)
        {
            //clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //release com objects to fully kill excell process
            if (xlRange != null)
            { Marshal.ReleaseComObject(xlRange); } //kill the range..
            if (xlSheet != null)
            { Marshal.ReleaseComObject(xlSheet); } //kill the sheet used
            if (xlBook != null)
            {
                xlBook.Close(saveFile, Type.Missing, Type.Missing);
                Marshal.ReleaseComObject(xlBook); //kill the file
            }
            if (excelApp != null)
            {
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp); //kill the excel
            }
        }
        /// <summary>
        /// save and close the excel.
        /// </summary>
        /// <param name="saveFile">if is true, will save the file</param>
        /// <param name="fileName">string, the file name of the excel file</param>
        /// <param name="directory">string, the address where to save the excel</param>
        public void CloseExcelApp(bool saveFile, string fileName, string directory)
        {
            //clean up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //release com objects to fully kill excell process
            if (xlRange != null)
            { Marshal.ReleaseComObject(xlRange); } //kill the range..
            if (xlSheet != null)
            { Marshal.ReleaseComObject(xlSheet); } //kill the sheet used
            if (xlBook != null)
            {
                xlBook.Close(saveFile, fileName, directory);
                Marshal.ReleaseComObject(xlBook); //kill the file
            }
            if (excelApp != null)
            {
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp); //kill the excel
            }
        }
        #endregion
    }
}