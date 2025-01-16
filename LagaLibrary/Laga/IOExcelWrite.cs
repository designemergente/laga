using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Laga.GeneticAlgorithm;

namespace Laga.IO
{
    /// <summary>
    /// Write data in excel
    /// </summary>
    public class IOExcelWrite
    {
        private string filePath;
        private int sheetNum;
        private Excel.Application excelApp;
        private Excel.Workbook xlBook;
        private Excel.Worksheet xlSheet;
        private readonly Excel.Range xlRange;
        private List<List<string>> dataExcel = new List<List<string>>();
        private List<string> lstDataExcel = new List<string>();
        private readonly object misValue = System.Reflection.Missing.Value;

        #region public properties
        /// <summary>
        /// Get and set the excel range.
        /// </summary>
        public Excel.Range XlRange
        {
            set => _ = xlRange;
            get => xlRange;
        }
        /// <summary>
        /// get the sheet number 
        /// </summary>
        public int SheetNum
        {
            get
            {
                return sheetNum;
            }
        }
        /// <summary>
        /// Set a nested list of strings.
        /// </summary>
        public List<List<string>> MatrixDataExcel
        {
            set
            {
                dataExcel = value;
            }
        }
        /// <summary>
        /// Set a nested list of strings.
        /// </summary>
        public List<string> ListDataExcel
        {
            set
            {
                lstDataExcel = value;
            }
        }
        /// <summary>
        /// check if the file exists.
        /// </summary>
        /// <param name="path">the path to the excel file</param>
        /// <returns>bool</returns>
        public static bool CheckExcelFile(string path)
        {
            return File.Exists(path);
        }
        #endregion
        #region constructors
        /// <summary>
        /// Creates and excel workbook.
        /// do not forget call the open and close.
        /// </summary>
        /// <param name="display">true, will display the workboook</param>
        public IOExcelWrite(bool display)
        {
            IOWrite_CreatesExcelApp(display);
        }
        /// <summary>
        /// Sets the basic data to write in excel.
        /// do not forget call the open and close.
        /// </summary>
        /// <param name="FilePath">adress to the file</param>
        public IOExcelWrite(string FilePath)
        {
            this.filePath = FilePath;
            if(IOExcel.TestExcelOpen(filePath, out xlBook))
            {
                //xlSheet = xlBook.ActiveSheet;
                xlSheet = xlBook.get_ActiveSheet();
            }
        }

        /// <summary>
        /// Creates an excel workbook, don't forget to save and close with the right methods.
        /// </summary>
        /// <param name="FolderPath">Folder path to save the excel doc</param>
        /// <param name="FileName">the name for the excel workbook</param>
        /// <param name="SheetName">The name of the sheet in the excel</param>
        /// <param name="Display">Boolean to see the document created</param>
        public IOExcelWrite(string FolderPath, string FileName, string SheetName = "My sheet", bool Display = true)
        {
            IOWrite_CreatesExcelBook(FolderPath, FileName, SheetName, Display);
        }
        /// <summary>
        /// Sets the basic data to write in excel.
        /// do not forget call the open and close.
        /// </summary>
        /// <param name="FilePath">the direction to the file</param>
        /// <param name="SheetNumber">the number of the sheet to open</param>
        public IOExcelWrite(string FilePath, int SheetNumber)
        {
            this.filePath = FilePath;
            this.sheetNum = SheetNumber;
        }
        #endregion

        /// <summary>
        /// Creates and excel sheet to write on.
        /// </summary>
        /// <param name="pos">The sheet position number</param>
        /// <param name="name">The sheet name name</param>
        public void IOWrite_NewExcelSheet(int pos, string name)
        {
     
            //xlSheet = (Excel.Worksheet)xlBook.Sheets.Add(Type.Missing, xlBook.Sheets[pos], 1, Excel.XlSheetType.xlWorksheet);
            xlSheet = xlBook.get_Sheets().Add(Type.Missing, xlBook.get_Sheets()[pos], 1, Excel.XlSheetType.xlWorksheet);
            xlSheet.Name = name;
        }
        /// <summary>
        /// Activates a specific Excel sheet to write on.
        /// </summary>
        /// <param name="pos">The position of the excel sheet in the document.</param>
        public void IOWrite_SetActiveSheet(int pos)
        {
            //Excel.Sheets excelSheets = xlBook.Worksheets;
            Excel.Sheets excelSheets = xlBook.get_Worksheets();
            xlSheet = excelSheets.Item[pos];
            xlSheet.Activate();
            xlSheet.Visible = Excel.XlSheetVisibility.xlSheetVisible;
        }
        #region methods to write excel
        /// <summary>
        /// Write a single item to Excel...
        /// </summary>
        /// <param name="strItem">the item to write in excel</param>
        /// <param name="Row">The row position</param>
        /// <param name="Col">The column position</param>
        /// 

        public void IOWriteItem(string strItem, int Row, int Col)
        {
            xlSheet.Cells[Row, Col] = strItem;
        }
        /// <summary>
        /// Write a list of strings in excel. the list length is calculated automatically.
        /// </summary>
        /// <param name="lstString">The list of strings to write</param>
        /// <param name="Row">The row position</param>
        /// <param name="Col">The column position</param>
        /// <param name="dir">The writing direction, 'c' for top-down or 'r' for left-right</param>
        public void IOWriteList(List<string> lstString, int Row, int Col, char dir)
        {
            if (dir == 'c' || dir == 'r')
            {
                foreach (string s in lstString)
                {
                    xlSheet.Cells[Row, Col] = s;
                    if (dir == 'c')
                    {
                        Row++;
                    }
                    else
                    {
                        Col++;
                    }
                }
            }
        }

        /* should used to write a population in excel.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="chromosomes"></param>
        /// <param name="Row"></param>
        /// <param name="Col"></param>
        /// <param name="swap"></param>
        public static bool WritePopulation<T>(Population<T> chromosomes, int Row = 1, int Col = 1, bool swap = true)
        {
            
            try
            {
                if (chromosomes.Count > 0)
                {
                    int origCol = Col;
                    int origRow = Row;
                    if (swap)
                    {
                        foreach (List<T> lstString in chromosomes)
                        {
                            Col = origCol;
                            foreach (T s in lstString)
                            {
                                xlSheet.Cells[Row, Col] = s;
                                Col++;
                            }
                            Row++;
                        }
                    }
                    else
                    {
                        foreach (List<T> lstString in chromosomes)
                        {
                            Row = origRow;
                            foreach (T s in lstString)
                            {
                                xlSheet.Cells[Row, Col] = s;
                                Row++;
                            }
                            Col++;
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        */
        
        /// <summary>
        /// Write a nested list of strings in excel. The list length is calculated automatically.
        /// </summary>
        /// <param name="matData">The nested list of strings to write</param>
        /// <param name="Row">The row position</param>
        /// <param name="Col">The column position</param>
        /// <param name="swap">If true, write first the row, if false write first the column</param>
        /// 
        public void IOWriteMatrix(List<List<string>> matData, int Row, int Col, bool swap = true)
        {
            if (matData.Count > 0)
            {
                int origCol = Col;
                int origRow = Row;
                if (swap)
                {
                    foreach (List<string> lstString in matData)
                    {
                        Col = origCol;
                        foreach (string s in lstString)
                        {
                            xlSheet.Cells[Row, Col] = s;
                            Col++;
                        }
                        Row++;
                    }
                }
                else
                {
                    foreach (List<string> lstString in matData)
                    {
                        Row = origRow;
                        foreach (string s in lstString)
                        {
                            xlSheet.Cells[Row, Col] = s;
                            Row++;
                        }
                        Col++;
                    }
                }
            }
        }
        #endregion
        #region Methods to Open Excel
        /// <summary>
        /// Open an Excel document and display the document.
        /// </summary>
        public void IOWrite_OpenExcelApp()
        {
            excelApp = new Excel.Application();
            //xlBook = excelApp.Workbooks.Open(filePath);
            xlBook = excelApp.get_Workbooks().Open(filePath);
            //excelApp.Visible = true;
            excelApp.set_Visible(true);
        }
        /// <summary>
        /// Open an Excel document based on the constructor.
        /// </summary>
        /// <param name="display">Show the app</param>
        /// <param name="sheetNum">open the sheetnumber</param>
        public void IOWrite_OpenExcelApp(bool display, int sheetNum)
        {
            excelApp = new Excel.Application();
            xlBook = excelApp.Workbooks.Open(filePath);
            if (display)
            {
                excelApp.Visible = display;
                excelApp.WindowState = Excel.XlWindowState.xlMaximized;
            }
            int c = excelApp.Worksheets.Count; //check the sheetnumber...
            sheetNum = c;
            if ((sheetNum > c) || (sheetNum < 1)) //means we need to creates a new excel sheet.
            {
                xlSheet = (Excel.Worksheet)xlBook.Sheets.Add(Type.Missing, xlBook.Sheets[c], 1, Excel.XlSheetType.xlWorksheet);
                xlSheet.Name = "Gner8At_" + DateTime.Now.ToString("ss.fff");
            }
            else
            {
                xlSheet = xlBook.Sheets[sheetNum];
            }
        }
        /// <summary>
        /// Open an Excel document based on the constructor.
        /// </summary>
        /// <param name="display">if is true, show the excel"</param>
        public void IOWrite_OpenExcelApp(bool display)
        {
            excelApp = new Excel.Application();
            xlBook = excelApp.Workbooks.Open(filePath);
            if (display)
            {
                excelApp.Visible = display;
                excelApp.WindowState = Excel.XlWindowState.xlMaximized;
            }
            int c = excelApp.Worksheets.Count; //check the sheetnumber...
            sheetNum = c;
            if ((sheetNum > c) || (sheetNum < 1)) //means we need to creates a new excel sheet.
            {
                xlSheet = (Excel.Worksheet)xlBook.Sheets.Add(Type.Missing, xlBook.Sheets[c], 1, Excel.XlSheetType.xlWorksheet);
            }
            else
            {
                xlSheet = xlBook.Sheets[sheetNum];
            }
        }
        #endregion
        private void IOWrite_CreatesExcelApp(bool display)
        {
            excelApp = new Excel.Application();
            xlBook = excelApp.Workbooks.Add(misValue);
            excelApp.Visible = display;
        }
        private bool IOWrite_CreatesExcelBook(string folderPath, string fileName, string sheetName, bool display)
        {
            excelApp = new Excel.Application();

            if (excelApp == null)
            {
                // Excel is not installed, handle this case accordingly.
                return false;
            }

            xlBook = excelApp.Workbooks.Add();
            xlSheet = (Excel.Worksheet)xlBook.Worksheets.Add();
            xlSheet.Name = sheetName;

            // You can add data to the worksheet here, e.g., worksheet.Cells[1, 1] = "Hello, World";

            this.filePath = Path.Combine(folderPath, fileName + ".xlsx");
            xlBook.SaveAs(filePath);
            excelApp.Visible = display;

            return true;
        }
        /// <summary>
        /// Save the Excel file and close safely the Excel application.
        /// </summary>
        /// <param name="Path">The file name</param>
        public void SaveCloseExcelApp(string Path)
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
                xlBook.SaveAs(Path);
                xlBook.Close(true, misValue, misValue);
                Marshal.ReleaseComObject(xlBook); //kill the file
            }
            if (excelApp != null)
            {
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp); //kill the excel
            }
        }
        /// <summary>
        /// Close safely the Excel application and decide to save or not.
        /// </summary>
        /// <param name="saveFile">If true, the Excel file is saved.</param>
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
    }
}
