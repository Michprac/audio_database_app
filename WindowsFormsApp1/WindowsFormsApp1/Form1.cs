using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)// tworzenie zmiennych, którym nadamy wartosci pól tekstowych
        {
            string gatunek = textBox1.Text;
            string autor = textBox2.Text;
            string ilosc_osob = textBox3.Text;
            string rok_wydania = textBox3.Text;
            string dlugosc = textBox3.Text;
            dataGridView1.Rows.Add(gatunek, autor, ilosc_osob, rok_wydania, dlugosc);// dodanie do tabeli naszych danych
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();// usunięcie wszystkich danych
        }

        private void button3_Click(object sender, EventArgs e)//usunięcie zaznaczonego wierszu
        {
            int ind = dataGridView1.SelectedCells[0].RowIndex;//usunięcie wynika za pomocą indeksu
            dataGridView1.Rows.RemoveAt(ind);
        }

        private void otworzycToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream mystr = null;//Używa się biblioteki System.IO, która pracuje z plikami; Także dodaje się element uprawlenie "dialogowe okienko" dla otwierania i zapisywania(openFileDialog1, saveFileDialog1)
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((mystr = openFileDialog1.OpenFile()) != null)
                {
                    StreamReader myread = new StreamReader(mystr);
                    string[] str;
                    int num = 0;
                    try
                    {
                        string[] str1 = myread.ReadToEnd().Split('\n');//tworzymy macierz danych z wierszu, ktory bedzie ładowac sie z pliku. Jak to sie dziala. laduje sie plik i dzieli sie za pomoca symbola \n 
                        num = str1.Count();
                        dataGridView1.RowCount = num;
                        for (int i = 0; i < num; i++)
                        {
                            str = str1[i].Split('^');// Tym wierszem bierzemy jeden z elementów macierzy i dzielimy na macierz za pomocą symbola ^
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                try
                                {
                                    dataGridView1.Rows[i].Cells[j].Value = str[j];
                                }
                                catch { }
                            }
                        }
                    }
                    catch (Exception ex)// Obrabia wyjątki. W rezultacie błędu, na ekranie będzie pojawiał się błąd, który się otrzymał
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally// Zamknięcie plików które były otwarte na edytowanie
                    {
                        myread.Close();
                    }
                }
            }
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)//Tutaj kod jest podobny
        {
            Stream myStream;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter myWritet = new StreamWriter(myStream);
                    try
                    {
                        for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                        {
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                myWritet.Write(dataGridView1.Rows[i].Cells[j].Value.ToString() + "^");
                            }
                            myWritet.WriteLine();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        myWritet.Close();
                    }
                    myStream.Close();
                }
            }
        }

       

       
    }
}
