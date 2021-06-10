using RochesterConverter.Application.Interface;
using RochesterConverter.Domain;
using RochesterConverter.Presentation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace RochesterConverter
{
    public partial class MainForm : Form
    {
        private readonly ICSVFactory _CSVFactory;
        private readonly IPDFToImageConverterService _PDFToImageConverterService;
        private readonly IValidateService _validateService;
        private readonly string[] _InputTextBoxText = { "", "", "Date", "Customer","", "UDF doc", "MAS PO", "UDF PO", "Item code", "Qty", "", "", };

        public MainForm(ICSVFactory iCSVFactory, IPDFToImageConverterService iPDFToImageConverterService, IValidateService validateService)
        {
            InitializeComponent();
            _CSVFactory = iCSVFactory;
            _PDFToImageConverterService = iPDFToImageConverterService;
            _validateService = validateService;
        }

        private void saveCsvMenuStrip_Click(object sender, EventArgs e)
        {
            var csvString = _CSVFactory.CreateCSVData(GetListViewStringListData(orderListView));
            SaveFileDialog path = new SaveFileDialog();
            path.Filter = "|*.csv";
            if (path.ShowDialog() == DialogResult.OK)
            {
                _CSVFactory.SaveCSV(path.FileName, csvString);
            }
        }

        private void openPdfMenuStrip_Click(object sender, EventArgs e)
        {
            OpenFileDialog path = new OpenFileDialog();
            path.Filter = "|*.pdf";
            if (path.ShowDialog() == DialogResult.OK)
            {
                orderListView.Clear();
                errorListView.Clear();
                TextBoxTextClear();
                label1.Text = "Loading...";
                label1.Refresh();
                var images = _PDFToImageConverterService.LoadPdfAsImage(path.FileName);
                var number = _PDFToImageConverterService.SaveImages(images);
                images.ForEach(x => x.Dispose());
                var ocrString = _CSVFactory.OCRImages(number);
                var order = _CSVFactory.MakeOrderData(ocrString);

                LoadOrderListViewData(order);
                LoadErrorListViewData();
            }
            label1.Text = string.Empty;
        }

        private void LoadOrderListViewData(Order order)
        {
            orderListView.Clear();
            orderListView.View = View.Details;
            orderListView.FullRowSelect = true;
            orderListView.GridLines = true;
            var line = _CSVFactory.CreateListViewData(order);

            foreach (var row in line)
            {
                orderListView.Items.Add(new ListViewItem(row));
            }
            CreateColumnsAllListView();
            orderListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void LoadErrorListViewData()
        {
            errorListView.Clear();
            errorListView.View = View.Details;
            
            foreach (var item in _validateService.GetErrors(GetListViewStringListData(orderListView)))
            {
                errorListView.Items.Add(item.Errortext);
                orderListView.Items[item.RowIndex].BackColor = Color.IndianRed;
            }
            errorListView.Columns.Add("Errors:", 100);
            orderListView.Items[0].BackColor = Color.White;
            orderListView.Refresh();
            errorListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        private void ListView1_Click(object sender, EventArgs e)
        {
            TextBoxTextClear();
            if (orderListView.Items[0].Selected)
            {
                ItemRowTextBoxVisible(false);
                CustomerRowTextBoxVisible(false);
            }
            else if (orderListView.Items[1].Selected)
            {
                ItemRowTextBoxVisible(false);
                CustomerRowTextBoxVisible(true);
                orderDateTextBox.Text = orderListView.SelectedItems[0].SubItems[2].Text;
                customerTextBox.Text = orderListView.SelectedItems[0].SubItems[3].Text;
                UDFDocTextBox.Text = orderListView.SelectedItems[0].SubItems[5].Text;
                MASPOTextBox.Text = orderListView.SelectedItems[0].SubItems[6].Text;
                UDFPOTextBox.Text = orderListView.SelectedItems[0].SubItems[7].Text;
            }
            else
            {
                ItemRowTextBoxVisible(true);
                CustomerRowTextBoxVisible(false);
                itemCodeTextBox.Text = orderListView.SelectedItems[0].SubItems[8].Text;
                qtyTextBox.Text = orderListView.SelectedItems[0].SubItems[9].Text;
            }
        }
        private void ModifyButton_Click(object sender, EventArgs e)
        {
            if (orderListView.SelectedIndices.Count == 0)
            {
                MessageBox.Show("A row must be selected for modification.");
            }
            else
            {
                if (orderListView.Items[0].Selected)
                {
                    MessageBox.Show("The first row cannot be modified.");
                }
                else if (orderListView.Items[1].Selected)
                {
                    ValidationErrorMessage(2, orderDateTextBox.Text);
                    ValidationErrorMessage(3, customerTextBox.Text);
                    ValidationErrorMessage(5, UDFDocTextBox.Text);
                    ValidationErrorMessage(6, MASPOTextBox.Text);
                    ValidationErrorMessage(7, UDFPOTextBox.Text);
                }
                else
                {
                    ValidationErrorMessage(8, itemCodeTextBox.Text);
                    ValidationErrorMessage(9, qtyTextBox.Text);
                }
            }
            TextBoxTextClear();
            LoadErrorListViewData();
            orderListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            orderListView.SelectedIndices.Clear();
        }
        private void orderListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (orderListView.Items[0].Selected)
            {
                MessageBox.Show("The first row cannot be modified.");
            }
            else
            {
                var index = orderListView.SelectedItems[0].SubItems.IndexOf(orderListView.GetItemAt(e.X, e.Y).GetSubItemAt(e.X, e.Y));

                if(CellModificationItemCodeRow(index) || CellModificationHeader(index))
                {
                    ValidationErrorMessageWithInputBox(index);
                }
                else
                {
                    MessageBox.Show("This cell cannot be modified.");
                }
            }
            TextBoxTextClear();
            LoadErrorListViewData();
            orderListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            orderListView.SelectedIndices.Clear();
        }
        public bool CellModificationHeader(int index)
        {
            return (orderListView.Items[1].Selected && (index == 2 || index == 3 || index == 5 || index == 6 || index == 7));
        }
        public bool CellModificationItemCodeRow(int index)
        {
            return (!orderListView.Items[1].Selected && (index == 8 || index == 9 ));
        }

        private void ValidationErrorMessage(int index, string textBoxText)
        {
            if (_validateService.ValidateByIndex(index, textBoxText))
            {
                if (index == 2)
                {
                    var date = DateTime.Parse(textBoxText);
                    textBoxText = $"{date.Month}/{date.Day}/{date.Year}";
                }
                orderListView.SelectedItems[0].SubItems[index].Text = textBoxText;
                orderListView.SelectedItems[0].BackColor = Color.White;
            }
            else
            {
                MessageBox.Show(_validateService.GetErrorMessageTextByIndex(index));
            }
        }
        private void ValidationErrorMessageWithInputBox(int index)
        {
            string inputText = string.Empty;
            var inputBox = new InputBox();
            var dialogResult = inputBox.Show(_InputTextBoxText[index], ref inputText);
            if (dialogResult==DialogResult.Cancel)
                return;

            ValidationErrorMessage(index, inputText);
        }
        
        private void CreateColumnsAllListView()
        {
            orderListView.Columns.Add("", 100);
            orderListView.Columns.Add("", 100);
            orderListView.Columns.Add("", 100);
            orderListView.Columns.Add("", 60);
            orderListView.Columns.Add("", 120);
            orderListView.Columns.Add("", 150);
            orderListView.Columns.Add("", 150);
            orderListView.Columns.Add("", 170);
            orderListView.Columns.Add("", 60);
            orderListView.Columns.Add("", 60);
            orderListView.Columns.Add("", 60);
            orderListView.Columns.Add("", 60);
        }

        private void TextBoxTextClear()
        {
            orderDateTextBox.Text = String.Empty;
            customerTextBox.Text = String.Empty;
            UDFDocTextBox.Text = String.Empty;
            MASPOTextBox.Text = String.Empty;
            UDFPOTextBox.Text = String.Empty;
            itemCodeTextBox.Text = String.Empty;
            qtyTextBox.Text = String.Empty;
        }

        private IEnumerable<IEnumerable<string>> GetListViewStringListData(ListView listView)
        {
            return listView.Items.Cast<ListViewItem>().Select(x => x.SubItems.Cast<ListViewSubItem>().Select(x => x.Text));
        }

        private void ItemRowTextBoxVisible(bool logic)
        {
            itemCodeTextBox.Visible = logic;
            qtyTextBox.Visible = logic;
        }

        private void CustomerRowTextBoxVisible(bool logic)
        {
            orderDateTextBox.Visible = logic;
            customerTextBox.Visible = logic;
            UDFDocTextBox.Visible = logic;
            MASPOTextBox.Visible = logic;
            UDFPOTextBox.Visible = logic;
        }
    }
}
