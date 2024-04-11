using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace work59
{
    public partial class Form1 : Form
    {
        private int supporNumber;
        private DataTable Table;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Решение системы уравнений";
            label1.Text = "Ведите количество неизвестных:";
            button1.Text = "Ввести";
            textBox1.TabIndex = 0;
            dataGridView1.Visible = false;

            Table = new DataTable();
        }


        private void button_Click(object sender, EventArgs e)
        {
            int i, j;
            Double[,] A;
            Double[] L;
            bool NumberOrNull = false;
            string tmp = "временная рабочая переменная";

            if (button1.Text == "Ввести")
            {
                for (; ; )
                {
                    NumberOrNull = int.TryParse(textBox1.Text, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out supporNumber);
                    if (NumberOrNull == false) return;

                    button1.Text = "Решить";
                    textBox1.Enabled = false;
                    dataGridView1.Visible = true;
                    dataGridView1.DataSource = Table;

                    for (i = 1; i <= supporNumber; i++)
                    {
                        tmp = "X" + Convert.ToString(i);
                        Table.Columns.Add(new DataColumn(tmp));
                    }

                    Table.Columns.Add(new DataColumn("L"));
                    return;
                }
            }
            else
            {
                if (Table.Rows.Count != supporNumber)
                {
                    MessageBox.Show(
                    "Количество строк не равно количеству колонок");
                    return;
                }
                A = new Double[supporNumber, supporNumber];
                L = new Double[supporNumber];

                for (j = 0; j <= supporNumber - 1; j++)
                {
                    for (i = 0; i <= supporNumber - 1; i++)
                    {
                        A[j, i] = ReturnNumber(j, i, ref NumberOrNull);
                        if (NumberOrNull == false) return;
                    }
                    L[j] = ReturnNumber(j, i, ref NumberOrNull);

                    if (NumberOrNull == false) return;
                }
            }
            gauss(supporNumber, A, ref L);
            string s = "Неизвестные равны:\n";

            for (j = 1; j <= supporNumber; j++)
            {
                tmp = L[j - 1].ToString();
                s = s + "X" + j.ToString() + " = " + tmp + ";\n";
            }
            MessageBox.Show(s);
        }

        private Double ReturnNumber(int j, int i, ref Boolean NumberOrNull)
        {
            Double work;
            string tmp = Table.Rows[j][i].ToString();
            NumberOrNull = Double.TryParse(tmp, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out work);

            if (NumberOrNull == false)
            {
                tmp = String.Format("Номер строки {0}, номер столбца " + "{1}," + "\n в данном поле - не число", j + 1, i + 1);
                MessageBox.Show(tmp);
            }
            return work;
        }
        private void gauss(int n, double[,] A, ref double[] LL)
        {
            int i, j, l = 0;
            Double c1, c2, c3;

            for (i = 0; i <= n - 1; i++)
            {
                c1 = 0;

                for (j = i; j <= n - 1; j++)
                {
                    c2 = A[j, i];

                    if (Math.Abs(c2) > Math.Abs(c1))
                    {
                        l = j; c1 = c2;
                    }
                }

                for (j = i; j <= n - 1; j++)
                {
                    c3 = A[l, j] / c1;
                    A[l, j] = A[i, j]; A[i, j] = c3;
                }
                c3 = LL[l] / c1; LL[l] = LL[i]; LL[i] = c3;

                for (j = 0; j <= n - 1; j++)
                {
                    if (j == i) continue;
                    for (l = i + 1; l <= n - 1; l++)
                    {
                        A[j, l] = A[j, l] - A[i, l] * A[j, i];
                    }
                    LL[j] = LL[j] - LL[i] * A[j, i];
                }
            }
        }
    }
}


