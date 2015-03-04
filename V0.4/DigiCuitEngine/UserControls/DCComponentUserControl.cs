using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigiCuitEngine.UserControls
{
    public enum ControlType { Null, Button, Check, Slider, Number }

    public partial class DCComponentUserControl : UserControl
    {
        private ControlType _type;
        private Jint.Native.JsValue _action;
        private Jint.Native.JsValue[] _args;

        private Control _ctrl;

        public string Label
        {
            get
            {
                string val = "";
                switch (_type)
                {
                    case ControlType.Button: val = _ctrl.Text; break;
                    case ControlType.Check: val = _ctrl.Text; break;
                    case ControlType.Null: val = _ctrl.Text; break;
                    case ControlType.Number: val = _ctrl.Controls[1].Text; break;
                    case ControlType.Slider: val = _ctrl.Controls[1].Text; break;
                }
                return val;
            }
            set
            {
                switch (_type)
                {
                    case ControlType.Button: _ctrl.Text = value; break;
                    case ControlType.Check: _ctrl.Text = value; break;
                    case ControlType.Null: _ctrl.Text = value; break;
                    case ControlType.Number: _ctrl.Controls[1].Text = value; break;
                    case ControlType.Slider: _ctrl.Controls[1].Text = value; break;
                }
            }
        }

        public object Value
        {
            get
            {
                object val = "";
                switch (_type)
                {
                    case ControlType.Button: val = _ctrl.Tag; break;
                    case ControlType.Check: val = ((CheckBox)_ctrl).Checked; break;
                    case ControlType.Null: val = _ctrl.Tag; break;
                    case ControlType.Number: val = ((NumericUpDown)_ctrl.Controls[0]).Value; break;
                    case ControlType.Slider: val = ((TrackBar)_ctrl.Controls[0]).Value; break;
                }
                return val;
            }
            set
            {
                try
                {

                    switch (_type)
                    {
                        case ControlType.Button: _ctrl.Tag = value; break;
                        case ControlType.Check: ((CheckBox)_ctrl).Checked = Boolean.Parse(value.ToString()); break;
                        case ControlType.Null: _ctrl.Tag = value; break;
                        case ControlType.Number: ((NumericUpDown)_ctrl.Controls[0]).Value = Decimal.Parse(value.ToString()); break;
                        case ControlType.Slider: ((TrackBar)_ctrl.Controls[0]).Value = Int32.Parse(value.ToString()); break;
                    }
                }
                catch (Exception ex) 
                { MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Value Set Failed", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        public ControlType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                switch (value)
                {
                    case ControlType.Button: MakeButton(); break;
                    case ControlType.Check: MakeCheck(); break;
                    case ControlType.Null: MakeNull(); break;
                    case ControlType.Number: MakeNumber(); break;
                    case ControlType.Slider: MakeSlider(); break;
                }
            }
        }

        private void MakeSlider()
        {
            _ctrl = new TrackBar();
            ((TrackBar)_ctrl).ValueChanged += new EventHandler(_ctrl_Do);
            ((TrackBar)_ctrl).AutoSize = false;
            ((TrackBar)_ctrl).Dock = DockStyle.Bottom;
            this.Controls.Add(_ctrl);
            this.Controls.Add(new Label());
        }

        private void MakeNumber()
        {
            _ctrl = new NumericUpDown();
            ((NumericUpDown)_ctrl).ValueChanged += new EventHandler(_ctrl_Do);
            ((NumericUpDown)_ctrl).AutoSize = false;
            ((NumericUpDown)_ctrl).Dock = DockStyle.Bottom;
            this.Controls.Add(_ctrl);
            this.Controls.Add(new Label());
        }

        private void MakeNull()
        {
            _ctrl = new Label();
            _ctrl.Text = "Type is Null";
            ((Label)_ctrl).AutoSize = false;
            ((Label)_ctrl).Dock = DockStyle.Fill;
            this.Controls.Add(_ctrl);
        }

        private void MakeCheck()
        {
            _ctrl = new CheckBox();
            _ctrl.Click += new EventHandler(_ctrl_Do);
            ((CheckBox)_ctrl).AutoSize = false;
            ((CheckBox)_ctrl).Dock = DockStyle.Fill;
            this.Controls.Add(_ctrl);
        }

        private void MakeButton()
        {
            _ctrl = new Button();
            _ctrl.Click += new EventHandler(_ctrl_Do);
            ((Button)_ctrl).AutoSize = false;
            ((Button)_ctrl).Dock = DockStyle.Fill;
            this.Controls.Add(_ctrl);
        }

        void _ctrl_Do(object sender, EventArgs e)
        {
            try { _action.Invoke(_args); }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public DCComponentUserControl()
        {
            InitializeComponent();
            _ctrl = new Control();
        }

        internal static Control GenerateUserControl(ControlType ControlType, string Label, object Value, Jint.Native.JsValue Action, Jint.Native.JsValue[] Args)
        {
            DCComponentUserControl ctrl = new DCComponentUserControl();

            ctrl.Type = ControlType;
            ctrl.Label = Label;
            ctrl.Value = Value;
            ctrl._action = Action;
            ctrl._args = Args;

            return ctrl;
        }
    }
}
