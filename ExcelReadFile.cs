//===================        Links       ===========================================
1. 
https://www.add-in-express.com/creating-addins-blog/2013/11/05/release-excel-com-objects/

2.
http://techbrij.com/export-excel-xls-xlsx-asp-net-npoi-epplus   --  SEEMS REALLY GOOD

3. Connection strings
https://www.connectionstrings.com/excel-2007/

4. Import Excel Spreadsheet data into Sql Server table via C# and vb.net
https://code.msdn.microsoft.com/office/Import-Excel-Spreadsheet-2b7ca7cf

5.
https://bytescout.com/products/developer/spreadsheetsdk/read-write-excel.html

6. Inserting Form Data into Database and Display in ASP.NET - COULDNT GET IT TO LOG IN WITH CORRECT PASSWORD
http://www.c-sharpcorner.com/uploadfile/0c1bb2/inserting-form-data-into-database-and-display-it-in-gridview/

7. Inserting Excel File Records Into SQL Server Database Using ASP.Net C# - COULDNT GET IT TO WORK
http://www.c-sharpcorner.com/UploadFile/0c1bb2/inserting-excel-file-records-into-sql-server-database-using/

8.
http://www.aspsnippets.com/Articles/Read-and-Import-Excel-Sheet-using-ADO.Net-and-C.aspx
http://www.aspsnippets.com/Articles/Read-and-Import-Excel-File-into-DataSet-or-DataTable-using-C-and-VBNet-in-ASPNet.aspx

9. Read Excel in ASP.NET
http://www.codeproject.com/Tips/311014/READ-EXCEL-IN-ASPNET



//===================        Method   1   WORKED FOR ME    ===========================================
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;   


namespace ExcelDatabase
{
    class Program
    {
		//Create COM Objects. Create a COM object for everything that is referenced
		Excel.Application xlApp = new Excel.Application();
		Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\miroslaw.kaczor\Documents\ExcelDatabaseTest.xlsx");
		Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
		Excel.Range xlRange = xlWorksheet.UsedRange;

				//int rowCount = xlRange.Rows.Count;		//Apparantely you could use those two lines and replace 11 & 5 below for rowCount & colCount  -- NOT TESTED THO !
				//int colCount = xlRange.Columns.Count;
				
				
		for (int i = 1; i <= 11; i++)               // In place of 11 specify the number of rows
		{
			for (int j = 1; j <= 5; j++)            // in place of 5 specify the number of columns
			{
				//new line
				if (j == 1)
					Console.Write("\r\n");

				//write the value to the console
				if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
					Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");

				//add useful things here!   
			}
		}

		/* Lastly, the references to the unmanaged memory must be released. 
		 * If this is not properly done, then there will be lingering processes that will hold the file access writes to your Excel workbook. */

		/*  **Important**  You need to do clean up otherwise there will be several instances of Excel opened in Task Manager */
		Console.WriteLine("\n**Important**  You need to do clean up otherwise there will be several instances of Excel opened in Task Manager");

		//cleanup
		GC.Collect();
		GC.WaitForPendingFinalizers();

		//rule of thumb for releasing com objects:
		//  never use two dots, all COM objects must be referenced and released individually
		//  ex: [somthing].[something].[something] is bad

		//release com objects to fully kill excel process from running in the background
		Marshal.ReleaseComObject(xlRange);
		Marshal.ReleaseComObject(xlWorksheet);

		//close and release
		xlWorkbook.Close();
		Marshal.ReleaseComObject(xlWorkbook);

		//quit and release
		xlApp.Quit();
		Marshal.ReleaseComObject(xlApp);
	}
}
	
	
	
	