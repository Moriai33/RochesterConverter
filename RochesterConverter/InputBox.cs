using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RochesterConverter.Presentation
{
    internal class InputBox
    {
        private readonly Form _form;
        private readonly Label _label;
        private readonly TextBox _textBox;
        private readonly Button _buttonOk;
        private readonly Button _buttonCancel;

        public InputBox()
        {
            _form = new Form();
            _label = new Label();
            _textBox = new TextBox();
            _buttonOk = new Button();
            _buttonCancel = new Button();
            Initialize();
        }

        private void Initialize()
        {
            _buttonOk.Text = "Modify";
            _buttonCancel.Text = "Cancel";
            _buttonOk.DialogResult = DialogResult.OK;
            _buttonCancel.DialogResult = DialogResult.Cancel;

            _label.SetBounds(9, 20, 372, 13);
            _textBox.SetBounds(12, 36, 372, 20);
            _buttonOk.SetBounds(228, 72, 75, 23);
            _buttonCancel.SetBounds(309, 72, 75, 23);

            _label.AutoSize = true;
            _textBox.Anchor = _textBox.Anchor | AnchorStyles.Right;
            _buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            _form.ClientSize = new Size(396, 107);
            _form.Controls.AddRange(new Control[] { _label, _textBox, _buttonOk, _buttonCancel });
            _form.ClientSize = new Size(Math.Max(300, _label.Right + 10), _form.ClientSize.Height);
            _form.FormBorderStyle = FormBorderStyle.FixedDialog;
            _form.StartPosition = FormStartPosition.CenterScreen;
            _form.MinimizeBox = false;
            _form.MaximizeBox = false;
            _form.AcceptButton = _buttonOk;
            _form.CancelButton = _buttonCancel;
        }
        public DialogResult Show(string promptText, ref string value)
        {
            _label.Text = promptText;
            _textBox.Text = value;
            var dialogResult = _form.ShowDialog();
            
            value = _textBox.Text;

            return dialogResult;
        }
    }
}

