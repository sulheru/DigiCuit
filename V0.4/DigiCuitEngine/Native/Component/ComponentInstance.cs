using DigiCuitEngine.Interfaces;
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
            this.FastAddProperty("setLocation", new ClrFunctionInstance(_engine, setLocation), false, false, false);
            this.FastAddProperty("isActive", _isActive, true, false, true);
            this.FastAddProperty("run", new ClrFunctionInstance(_engine, _run), true, false, true);
            this.FastAddProperty("loadImageFile", new ClrFunctionInstance(_engine, loadImageFile), false, false, false);
        }

        private JsValue loadImageFile(JsValue thisObject, JsValue[] arguments)
        {
            if (arguments.Length == 1 && arguments.First().IsString())
            {
                FileInfo fi = null;
                try { fi = new FileInfo(arguments.First().ToString()); }
                catch (Exception e) { throw e; }
                if (fi != null && fi.Exists) { this.Image = new Bitmap(fi.FullName); }
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

        public string Id { get; protected set; }

        #endregion

        #region "Component Methods"

        public void Run()
        {
            var run = _engine.GetValue(this, "run");
            var callable = run.TryCast<ICallable>();
            if (callable == null)
            {
                throw new ArgumentException("Can only invoke functions");
            }
            var arguments = new JsValue[0];
            JsValue jsres = callable.Call(JsValue.FromObject(_engine, this), arguments);
            string res = jsres.ToString();
        }

        #endregion

        public Image Image { get; set; }
    }
}
