using DigiCuitEngine.Interfaces;
using DigiCuitEngine.UserControls;
using Jint.Native;
using Jint.Native.Object;
using Jint.Runtime;
using Jint.Runtime.Interop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCuitEngine.Native.Component
{
    public class ComponentInstance : Jint.Native.Object.ObjectInstance
    {
        #region "JsValue Containers"

        private Jint.Engine _engine;
        private JsValue _location;
        private JsValue _isActive;
        private JsValue _curImg;

        public System.Windows.Forms.Panel Controls { get; set; }
        public int CurrentImage
        {
            get { return Int32.Parse(_curImg.ToString()); }
            set { _curImg = value; }
        }

        #endregion

        #region "Js main methods"

        public ComponentInstance(Jint.Engine engine)
            : base(engine)
        {
            // Propiedades globales (statics)
            _engine = engine;
            this.Id = IComponent.GenerateUniqueId();
            ComponentFastConfigure();
        }

        private void ComponentFastConfigure()
        {
            this.FastAddProperty("location", _location, true, false, true);
            this.FastAddProperty("graph", _curImg, true, false, true);
            this.FastAddProperty("setLocation", new ClrFunctionInstance(_engine, setLocation), false, false, false);
            this.FastAddProperty("addControl", new ClrFunctionInstance(_engine, _addControl), false, false, false);
            this.FastAddProperty("isActive", _isActive, true, false, true);
            this.FastAddProperty("run", new ClrFunctionInstance(_engine, _run), true, false, true);
            this.FastAddProperty("controls", new ClrFunctionInstance(_engine, _controls), true, false, true);
            this.FastAddProperty("before", new ClrFunctionInstance(_engine, _before), true, false, true);
            this.FastAddProperty("after", new ClrFunctionInstance(_engine, _after), true, false, true);
            this.FastAddProperty("loadImageFile", new ClrFunctionInstance(_engine, _loadImageFile), false, false, false);
        }

        private JsValue _controls(JsValue arg1, JsValue[] arg2)
        {
            // throw new NotImplementedException();
            return JsValue.Undefined;
        }

        private JsValue _addControl(JsValue arg1, JsValue[] arg2)
        {
            ControlType ControlType;
            string Label = "No Label Text";
            object Value = "No Value";
            JsValue Action = JsValue.Null;
            JsValue[] Args = new JsValue[0];

            if (arg2.Length > 0)
            {
                string cTstr = arg2[0].ToString();
                ControlType = (cTstr.ToLower() == ControlType.Button.ToString().ToLower()) ? ControlType.Button : ControlType.Null;
                ControlType = (cTstr.ToLower() == ControlType.Check.ToString().ToLower()) ? ControlType.Check : ControlType.Null;
                ControlType = (cTstr.ToLower() == ControlType.Slider.ToString().ToLower()) ? ControlType.Slider : ControlType.Null;
                ControlType = (cTstr.ToLower() == ControlType.Number.ToString().ToLower()) ? ControlType.Number : ControlType.Null;
            }
            else { return JsValue.Null; }

            if (arg2.Length > 1)
            {
                if (arg2[1].IsString() || arg2[1].IsNumber())
                { Label = arg2[1].AsString(); }
            }

            if (arg2.Length > 2)
            {
                if (arg2[1].IsBoolean())
                { Value = arg2[2].AsBoolean(); }
                else if(arg2[2].IsString() || arg2[2].IsNumber())
                { Value = arg2[2].ToString(); }
            }

            if (arg2.Length > 3)
            { Action = arg2[3]; }

            if (arg2.Length > 4)
            {
                List<JsValue> args = new List<JsValue>();
                args.AddRange(arg2);
                Args = new JsValue[Args.Length - 4];
                args.CopyTo(4, Args, 0, Args.Length - 4);
            }

            this.Controls.Controls.Add(DCComponentUserControl.GenerateUserControl(ControlType, Label, Value, Action, Args));

            return JsValue.Undefined;
        }

        private JsValue _after(JsValue arg1, JsValue[] arg2)
        {
            // throw new NotImplementedException();
            return JsValue.Undefined;
        }

        private JsValue _before(JsValue arg1, JsValue[] arg2)
        {
            // throw new NotImplementedException();
            return JsValue.Undefined;
        }

        private JsValue _loadImageFile(JsValue thisObject, JsValue[] arguments)
        {
            if (arguments.Length == 1 && arguments.First().IsString())
            {
                FileInfo fi = null;
                try { fi = new FileInfo(arguments.First().ToString()); }
                catch (Exception e) { throw e; }
                if (fi != null && fi.Exists) { this.Images.Add(fi); }
                return this.Images.LastIndexOf(fi);
            }
            throw new ArgumentException("The arguments are not valid or file does not exist.");
        }

        private JsValue _run(JsValue thisObject, JsValue[] arguments)
        {
            // throw new NotImplementedException();
            return JsValue.Undefined;
        }

        private JsValue setLocation(JsValue thisObject, JsValue[] arguments)
        {
            if (arguments.Length == 2)
            {
                int x = TypeConverter.ToInt32(arguments.At(0));
                int y = TypeConverter.ToInt32(arguments.At(1));
                Location = new Point(x, y);
                return JsValue.True;
            }
            return JsValue.False;
        }

        public Point Location
        {
            get
            {
                return new Point(
                    Int32.Parse(_location.AsObject().Get("X").ToString()),
                    Int32.Parse(_location.AsObject().Get("Y").ToString()));
            }
            set
            {
                ObjectInstance obj = new ObjectInstance(_engine);
                obj.Put("X", new JsValue(value.X.ToString()), true);
                obj.Put("Y", new JsValue(value.Y.ToString()), true);
                _location = new JsValue(obj);
            }
        }

        public bool IsActive
        {
            get { return _isActive.IsBoolean() && _isActive.AsBoolean(); }
            set { _isActive = new JsValue(value); }
        }

        public override JsValue Call(JsValue thisObject, JsValue[] arguments)
        {
            var run = _engine.GetValue(this, thisObject.ToString());
            var callable = run.TryCast<ICallable>();
            if (callable == null)
            {
                throw new ArgumentException("Can only invoke functions");
            }
            JsValue jsres = callable.Call(JsValue.FromObject(_engine, this), arguments);
            return jsres;
        }

        public string Id { get; protected set; }

        #endregion

        #region "Component Methods"

        public void BeforeRun()
        {
            var run = _engine.GetValue(this, "before");
            var callable = run.TryCast<ICallable>();
            if (callable == null)
            {
                throw new ArgumentException("Can only invoke functions");
            }
            var arguments = new JsValue[0];
            JsValue jsres = callable.Call(JsValue.FromObject(_engine, this), arguments);
            string res = jsres.ToString();
        }

        public void Run()
        {
            var arguments = new JsValue[0];
            this.Call("before", arguments);
            this.Call("run", arguments);
            this.Call("after", arguments);
        }

        public System.Windows.Forms.Panel LoadControls()
        {
            var arguments = new JsValue[0];
            this.Controls.Controls.Clear();
            this.Call("controls", arguments);
            return this.Controls;
        }

        #endregion

        public List<FileInfo> Images { get; set; }
    }
}
