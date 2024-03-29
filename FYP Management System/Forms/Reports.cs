using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Font;

namespace FYP_Management_System.Forms
{
    public partial class Reports : Form
    {
        string title;
        public Reports()
        {
            InitializeComponent();
            title = "";
        }

        private void cmdReport1_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT PR.Title AS ProjectTitle, (SELECT FirstName + ' ' + LastName FROM Person P JOIN Advisor A ON A.Id = P.Id JOIN ProjectAdvisor PA ON A.Id = PA.AdvisorId WHERE AdvisorRole = 11 AND ProjectId = PR.Id) AS[Main Advisor], " + 
            "(SELECT FirstName + ' ' + LastName FROM Person P JOIN Advisor A ON A.Id = P.Id JOIN ProjectAdvisor PA ON A.Id = PA.AdvisorId WHERE AdvisorRole = 12 AND ProjectId = PR.Id) AS[Co - Advisor], (SELECT FirstName + ' ' + LastName FROM Person P JOIN Advisor A ON A.Id = P.Id " +
            "JOIN ProjectAdvisor PA ON A.Id = PA.AdvisorId WHERE AdvisorRole = 14 AND ProjectId = PR.Id) AS[Industry Advisor], FirstName + ' ' + LastName AS[Student Name] FROM Project AS PR LEFT JOIN GroupProject GP ON GP.ProjectId = PR.Id LEFT JOIN[Group] G ON G.Id = GP.GroupId " +
            "LEFT JOIN GroupStudent GS ON GS.GroupId = G.Id LEFT JOIN Student S ON S.Id = GS.StudentId JOIN Person P ON P.Id = S.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            title = cmdReport1.Text;
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            MainMenu form = new MainMenu();
            form.Show();
            this.Hide();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "PDF (*.pdf)|*.pdf";
                saveFile.FileName = "Report.pdf";
                bool error = false;

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(saveFile.FileName))
                    {
                        try
                        {
                            File.Delete(saveFile.FileName);
                        }
                        catch (Exception err)
                        {
                            error = true;
                            MessageBox.Show("Unable to write data. " + err.Message);
                        }
                    }

                    if (!error)
                    {
                        try
                        {
                            using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                            {
                                PdfWriter writer = new PdfWriter(stream);
                                using (PdfDocument pdf = new PdfDocument(writer))
                                {
                                    Document doc = new Document(pdf);
                                    PdfFont font = PdfFontFactory.CreateFont("Helvetica");
                                    Paragraph header = new Paragraph("Final Year Project Management System").SetTextAlignment(TextAlignment.CENTER).SetFontSize(28).SetFont(font);
                                    doc.Add(header);
                                    
                                    Paragraph reportTitle = new Paragraph(title).SetTextAlignment(TextAlignment.CENTER).SetFontSize(16).SetFont(font);
                                    doc.Add(reportTitle);

                                    Table pdfTable = new Table(UnitValue.CreatePercentArray(dataGridView1.ColumnCount)).UseAllAvailableWidth();

                                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                    {
                                        pdfTable.AddHeaderCell(new Cell().Add(new Paragraph(dataGridView1.Columns[i].HeaderText)));
                                    }

                                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                    {
                                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                        {
                                            pdfTable.AddCell(new Cell().Add(new Paragraph(dataGridView1[j, i]?.Value?.ToString() ?? "")));

                                        }
                                    }

                                    doc.Add(pdfTable);
                                }

                                MessageBox.Show("Report exported in PDF successfully.");
                            }
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show("Error while exporting report in PDF. Error: " + err.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Data.");
            }
        }

        private void cmdReport2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT P.Title AS Project, E.Name AS Evaluation, E.TotalMarks, GE.ObtainedMarks, S.RegistrationNo, PE.FirstName + ' ' + PE.LastName AS Name " +
            "FROM Project AS P INNER JOIN GroupProject AS GP ON GP.ProjectId = P.Id INNER JOIN [Group] AS G ON G.Id = GP.GroupId INNER JOIN GroupEvaluation AS GE ON GE.GroupId = G.Id INNER JOIN " +
            "Evaluation AS E ON E.Id = GE.EvaluationId LEFT OUTER JOIN GroupStudent AS GS ON GS.GroupId = G.Id INNER JOIN Student AS S ON S.Id = GS.StudentId INNER JOIN Person AS PE ON PE.Id = S.Id", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            title = cmdReport2.Text;
        }

        private void cmdReport3_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT S.Id, RegistrationNo, FirstName + ' ' + LastName AS [Name], (SELECT [Value] FROM Lookup WHERE Id = Gender AND Category = 'GENDER') AS [Gender], Contact, Email, DateOfBirth " +
            "FROM Person P JOIN Student S ON S.Id = P.Id LEFT JOIN GroupStudent GS ON GS.StudentId = S.Id WHERE GS.GroupId IS NULL", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            title = cmdReport3.Text;
        }

        private void cmdReport4_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT P.Title AS Project, SUM(TotalWeightage) AS[Weightage], CAST((SUM((GE.ObtainedMarks * E.TotalWeightage) / E.TotalMarks)) AS DECIMAL(5, 2)) AS[Weighted Marks] " + 
            "FROM Project P JOIN GroupProject GP ON P.Id = GP.ProjectId JOIN GroupEvaluation GE ON GP.GroupId = GE.GroupId JOIN Evaluation E ON GE.EvaluationId = E.Id GROUP BY P.Title ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            title = cmdReport4.Text;
        }

        private void cmdReport5_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("SELECT P.Title AS Project, SUM(E.TotalWeightage) AS [Weightage], (SELECT TOP 1 FirstName + ' ' + LastName FROM Person P1 JOIN Advisor A1 ON A1.Id = P1.Id JOIN ProjectAdvisor PA1 ON A1.Id = PA1.AdvisorId WHERE AdvisorRole = 11 AND ProjectId = P.Id) AS[Main Advisor], " +
            "(SELECT TOP 1 FirstName + ' ' + LastName FROM Person P2 JOIN Advisor A2 ON A2.Id = P2.Id JOIN ProjectAdvisor PA2 ON A2.Id = PA2.AdvisorId WHERE AdvisorRole = 12 AND ProjectId = P.Id) AS[Co - Advisor], (SELECT TOP 1 FirstName + ' ' + LastName FROM Person P3 JOIN Advisor A3 ON A3.Id = P3.Id JOIN " +
            "ProjectAdvisor PA3 ON A3.Id = PA3.AdvisorId WHERE AdvisorRole = 14 AND ProjectId = P.Id) AS[Industry Advisor] FROM Project P LEFT JOIN GroupProject GP ON P.Id = GP.ProjectId LEFT JOIN GroupEvaluation GE ON GP.GroupId = GE.GroupId LEFT JOIN Evaluation E ON GE.EvaluationId = E.Id GROUP BY P.Title, P.Id " +
            "HAVING SUM(E.TotalWeightage) IS NULL OR SUM(E.TotalWeightage) <> 100", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            title = cmdReport5.Text;
        }
    }
}
