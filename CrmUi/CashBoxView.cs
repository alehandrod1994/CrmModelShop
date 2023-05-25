using CrmBl.Model;
using System;
using System.Windows.Forms;

namespace CrmUi
{
    /// <summary>
    /// Панель отображения данных работы кассы.
    /// </summary>
    public class CashBoxView
    {
        /// <summary>
        /// Касса.
        /// </summary>
        private readonly CashDesk _cashDesk;

        /// <summary>
        /// Инициализирует новый экземпляр класса CashBoxView.
        /// </summary>
        /// <param name="cashDesk"> Касса. </param>
        /// <param name="number"> Номер кассы. </param>
        /// <param name="x"> Координата по X. </param>
        /// <param name="y"> Координата по Y. </param>
        public CashBoxView(CashDesk cashDesk, int number, int x, int y)
        {
            _cashDesk = cashDesk;

            CashDeskName = new Label();
            Price = new NumericUpDown();
            QueueLength = new ProgressBar();
            LeaveCustomersCount = new Label();

            CashDeskName.AutoSize = true;
            CashDeskName.Location = new System.Drawing.Point(x, y);
            CashDeskName.Name = "cashDeskName" + number;
            CashDeskName.Size = new System.Drawing.Size(35, 13);
            CashDeskName.TabIndex = number;
            CashDeskName.Text = cashDesk.ToString();

            Price.Location = new System.Drawing.Point(x + 70, y);
            Price.Name = "price" + number;
            Price.Size = new System.Drawing.Size(120, 20);
            Price.TabIndex = number;
            Price.Maximum = 10000000000000000;

            QueueLength.Location = new System.Drawing.Point(x + 250, y);
            QueueLength.Maximum = cashDesk.MaxQueueLength;
            QueueLength.Name = "queueLength" + number;
            QueueLength.Size = new System.Drawing.Size(100, 23);
            QueueLength.TabIndex = number;
            QueueLength.Value = 0;

            LeaveCustomersCount.AutoSize = true;
            LeaveCustomersCount.Location = new System.Drawing.Point(x + 400, y);
            LeaveCustomersCount.Name = "leaveCustomersCount" + number;
            LeaveCustomersCount.Size = new System.Drawing.Size(35, 13);
            LeaveCustomersCount.TabIndex = number;
            LeaveCustomersCount.Text = "";

            cashDesk.CheckClosed += CashDesk_CheckClosed;
        }

        /// <summary>
        /// Наименование кассы.
        /// </summary>
        public Label CashDeskName { get; set; }

        /// <summary>
        /// Выручка кассы.
        /// </summary>
        public NumericUpDown Price { get; set; }

        /// <summary>
        /// Длина очереди.
        /// </summary>
        public ProgressBar QueueLength { get; set; }

        /// <summary>
        /// Количество ушедших покупателей.
        /// </summary>
        public Label LeaveCustomersCount { get; set; }

        /// <summary>
		/// Обрабатывает событие, которое происходит после закрытия чека.
		/// </summary>
        private void CashDesk_CheckClosed(object sender, Check e)
        {
            Price.Invoke((Action)delegate 
            { 
                Price.Value += e.Price;
                QueueLength.Value = _cashDesk.Count;
                LeaveCustomersCount.Text = _cashDesk.ExitCustomer.ToString();
            });
        }
    }
}
