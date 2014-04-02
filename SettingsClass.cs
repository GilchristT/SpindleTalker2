using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using System.Drawing;
using System.ComponentModel;

namespace SpindleTalker2
{
    public static class Settings
    {
        private static Settings4Net _settings = new Settings4Net();
        private static string settingsFile;

        #region Setup MDI Forms

        public static MainWindow spindleTalkerBase = null;
        public static MeterControl graphsForm = new MeterControl();
        public static TerminalControl terminalForm = new TerminalControl();
        public static SettingsControl settingsForm = new SettingsControl();
        
        #endregion


        static Settings()
        {
            string settingsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SpindleTalker2");
            if (!Directory.Exists(settingsDir)) Directory.CreateDirectory(settingsDir);

            settingsFile = Path.Combine(settingsDir, "settings.xml");
            Console.WriteLine(settingsFile);

            if (File.Exists(settingsFile))
                _settings.Open(settingsFile);

            _VFD_MaxFreq = -1;
            _VFD_MinFreq = -1;
            _VFD_MaxRPM = -1;
        }

        public static bool SerialConnected
        {
            get { return _serialConnected;}
            set
            {
                _serialConnected = value;
                if(spindleTalkerBase != null)
                {
                    spindleTalkerBase.COMPortStatus(value);
                }
            }
        }
        private static bool _serialConnected;

        public static string PortName
        {
            get { return _settings.GetItemOrDefaultValue("PortName", "COM1").ToString(); }
            set { _settings.Settings["PortName"] = value; }
        }

        public static int BaudRate
        {
            get { return Convert.ToInt32(_settings.GetItemOrDefaultValue("BaudRate", 9600)); }
            set { _settings.Settings["BaudRate"] = value.ToString(); }
        }

        public static int DataBits
        {
            get { return Convert.ToInt32(_settings.GetItemOrDefaultValue("DataBits", 8)); }
            set { _settings.Settings["DataBits"] = value.ToString(); }
        }

        public static Parity Parity
        {
            get { return (Parity)Enum.Parse(typeof(Parity), _settings.GetItemOrDefaultValue("Parity", "None").ToString()); }
            set { _settings.Settings["Parity"] = value.ToString(); }
        }

        public static StopBits StopBits
        {
            get { return (StopBits)Enum.Parse(typeof(StopBits), _settings.GetItemOrDefaultValue("StopBits", 1).ToString()); }
            set { _settings.Settings["StopBits"] = value.ToString(); }
        }

        public static bool AutoConnectAtStartup
        {
            get { return Convert.ToBoolean(_settings.GetItemOrDefaultValue("AutoConnectAtStartup", false)); }
            set { _settings.Settings["AutoConnectAtStartup"] = value.ToString(); }
        }

        public static string LastMDIChild
        {
            get { return _settings.GetItemOrDefaultValue("LastMDIChild", "Graphs").ToString(); }
            set { _settings.Settings["LastMDIChild"] = value; }
        }

        public static int VFD_MinFreq
        {
            get { return _VFD_MinFreq; }
            set
            {
                _VFD_MinFreq = value;
                Settings.settingsForm.labelMinMaxFreq.Text = string.Format("Min/Max Frequency = {0}Hz/{1}Hz", _VFD_MinFreq, _VFD_MaxFreq);
            }
        }
        private static int _VFD_MinFreq;

        public static int VFD_MaxFreq
        {
            get { return _VFD_MaxFreq; }
            set
            {
                _VFD_MaxFreq = value;
                Settings.graphsForm.MeterOutF.ScaleMaxValue = _VFD_MaxFreq;
                Settings.graphsForm.MeterSetF.ScaleMaxValue = _VFD_MaxFreq;
                Settings.settingsForm.labelMinMaxFreq.Text = string.Format("Min/Max Frequency = {0}Hz / {1}Hz",_VFD_MinFreq, _VFD_MaxFreq);
            }
        }
        private static int _VFD_MaxFreq;

        public static int VFD_MaxRPM
        {
            get { return _VFD_MaxRPM; }
            set
            {
                _VFD_MaxRPM = value;
                Settings.graphsForm.MeterRPM.ScaleMaxValue = _VFD_MaxRPM;
                Settings.graphsForm.MeterRPM.ScaleMaxValue = _VFD_MaxRPM;
                Settings.settingsForm.labelMaxRPM.Text = string.Format("Maximum speed = {0:#,##0}RPM", _VFD_MaxRPM);
            }
        }
        private static int _VFD_MaxRPM;


        public static int VFD_MinRPM
        {
            get
            {
                if (VFD_MaxFreq > 0 && VFD_MaxRPM > 0)
                {
                    int minRPM = (int)(((double)VFD_MaxRPM / (double)VFD_MaxFreq) * (double)VFD_MinFreq);
                    return minRPM;
                }
                else return 0;
            }
            set { ; }
        }

        public static int VFD_ModBusID
        {
            get { return Convert.ToInt32(_settings.GetItemOrDefaultValue("VFD_ModBusID", 1)); }
            set { _settings.Settings["VFD_ModBusID"] = value.ToString(); }
        }

        public static string QuickSets
        {
            get { return _settings.GetItemOrDefaultValue("QuickSets", "6000;9000;15000;20000;24000").ToString(); }
            set { _settings.Settings["QuickSets"] = value; }
        }

        public static void Save()
        {
            _settings.Save(settingsFile);
        }

    }

    #region Settings4Net

    #region Property Grid generation related code (PropertyContainer/PropertyDescriptor)
#pragma warning disable 1591
    [System.Diagnostics.DebuggerNonUserCode]
    public class PropertyContainer : Dictionary<string, object>, ICustomTypeDescriptor
    {
        public PropertyContainer()
        {
        }

        #region Fields
        Dictionary<string, Property> propertiesDefinition = new Dictionary<string, Property>();
        #endregion

        public void AddProperty(Property property)
        {
            this.propertiesDefinition.Add(property.Name, property);
        }

        #region ICustomTypeDescriptor Members
        public AttributeCollection GetAttributes() { return TypeDescriptor.GetAttributes(this, true); }
        public string GetClassName() { return TypeDescriptor.GetClassName(this, true); }
        public string GetComponentName() { return TypeDescriptor.GetComponentName(this); }
        public TypeConverter GetConverter() { return TypeDescriptor.GetConverter(this, true); }
        public PropertyDescriptor GetDefaultProperty() { return TypeDescriptor.GetDefaultProperty(this, true); }
        public object GetEditor(System.Type editorBaseType) { return TypeDescriptor.GetEditor(this, editorBaseType, true); }
        public EventDescriptor GetDefaultEvent() { return TypeDescriptor.GetDefaultEvent(this, true); }
        public EventDescriptorCollection GetEvents(System.Attribute[] attributes) { return TypeDescriptor.GetEvents(this, attributes, true); }
        public EventDescriptorCollection GetEvents() { return TypeDescriptor.GetEvents(this, true); }

        public object GetPropertyOwner(PropertyDescriptor pd) { return this; }
        public PropertyDescriptorCollection GetProperties(System.Attribute[] attributes) { return GetProperties(); }

        public PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection pdc = new PropertyDescriptorCollection(null);
            foreach (KeyValuePair<string, object> prop in this)
            {
                Property property;
                if (this.propertiesDefinition.TryGetValue(prop.Key, out property))
                    pdc.Add(property);
            }
            return pdc;
        }
        #endregion
    }

    [System.Diagnostics.DebuggerNonUserCode]
    public class Property : PropertyDescriptor
    {
        static public SettingsHashtable Settings;

        #region Fields
        private Type type;
        private bool readOnly;
        public string _description;
        public string _category;
        public bool? _visible = null;
        #endregion

        public Property(string name, Type type, bool readOnly, string description, string category, bool? visible)
            : base(name, null)
        {
            this.type = type;
            this.readOnly = readOnly;
            this._description = description;
            this._category = category;
            this._visible = visible;
        }

        public override bool IsReadOnly { get { return this.readOnly; } }
        public override Type PropertyType { get { return this.type; } }
        public override Type ComponentType { get { throw new NotImplementedException(); } }
        public override string DisplayName { get { return this.Name; } }
        public override bool CanResetValue(object component) { return false; }
        public override void ResetValue(object component) { }
        public override bool ShouldSerializeValue(object component) { return true; }
        public override string Description { get { return this._description; } }
        public override string Category { get { return this._category; } }
        public override bool IsBrowsable
        {
            get
            {
                if (_visible == null)
                    return true;
                return _visible.Value;
            }
        }

        public override object GetValue(object component)
        {
            PropertyContainer container = component as PropertyContainer;
            if (container == null)
                throw new Exception("Invalid component type");
            return container[Name];
        }

        public override void SetValue(object component, object value)
        {
            if (this.readOnly)
                throw new InvalidOperationException();

            PropertyContainer container = component as PropertyContainer;
            if (container == null)
                throw new Exception("Invalid component type");

            container[Name] = value;
            Settings[Name] = value;
        }
    }
#pragma warning restore 1591
    #endregion

    /// <summary>
    /// Class containing meta data of Settings item
    /// </summary>
    public class SettingsMetaData
    {
        /// <summary>
        /// Settings item description
        /// </summary>
        public string Description = String.Empty;

        /// <summary>
        /// Settings item category
        /// </summary>
        public string Category = String.Empty;

        /// <summary>
        /// Settings item tag - could be used freely to identify object
        /// </summary>
        public string Tag = String.Empty;

        /// <summary>
        /// Settings item visibility - shows/hides it in PropertyGrid control
        /// </summary>
        public bool? Visible = null;

        /// <summary>
        /// SettingsMetaData Contructor
        /// </summary>
        /// <param name="Description">Settings item description</param>
        /// <param name="Category">Settings item category</param>
        /// <param name="Tag">Settings item tag</param>
        /// <param name="Visible">Settings item visibility</param>
        public SettingsMetaData(string Description, string Category, string Tag, bool? Visible)
        {
            this.Description = Description ?? String.Empty;
            this.Category = Category ?? String.Empty;
            this.Tag = Tag ?? String.Empty;
            this.Visible = Visible;
        }

        /// <summary>
        /// SettingsMetaData Constructor (creates default empty values)
        /// </summary>
        public SettingsMetaData()
        {
        }
    }


    /// <summary>
    /// Class for storing and handling a single Settings' item
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class Item<T>
    {
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="Name">Name of the settings item</param>
        /// <param name="Value">Value of the settings item</param>
        /// <param name="ModifiedDate">Date/Time of last modification</param>
        /// <param name="Readonly">Writing/changing permissions</param>
        /// <param name="MinValue">Minimum allowed value</param>
        /// <param name="MaxValue">Maximum allowed value</param>
        /// <param name="Default">Default value</param>
        /// <param name="MetaData">MetaData object</param>
        public Item(string Name, T Value, DateTime? ModifiedDate, bool? Readonly, T MinValue, T MaxValue, T Default, SettingsMetaData MetaData)
        {
            this.Name = Name;
            this.Value = Value;
            this.ModifiedDate = ModifiedDate;
            this.Readonly = Readonly;
            this.MinValue = MinValue;
            this.MaxValue = MaxValue;
            this.Default = Default;
            if (MetaData != null)
                this.MetaData = MetaData;
        }

        private SettingsMetaData _metaData = new SettingsMetaData();

        public SettingsMetaData MetaData
        {
            set
            {
                _metaData = value;
            }
            get
            {
                return _metaData;
            }
        }

        /// <summary>
        /// Name of the settings item
        /// </summary>
        public string Name { set; get; }

        private T _value = default(T);
        /// <summary>
        /// Value of the settings item. Has to be not null, not readonly and (if set) between [MinValue; MaxValue]
        /// </summary>
        public T Value
        {
            set
            {
                if (value != null && (Readonly == null || Readonly.Value == false))
                {
                    bool ok = true;
                    if (value is int?)
                    {
                        if (MinValue != null && (MinValue as int?).Value > (value as int?).Value)
                            ok = false;
                        if (MaxValue != null && (MaxValue as int?).Value < (value as int?).Value)
                            ok = false;
                    }
                    else if (value is double?)
                    {
                        if (MinValue != null && (MinValue as double?).Value > (value as double?).Value)
                            ok = false;
                        if (MaxValue != null && (MaxValue as double?).Value < (value as double?).Value)
                            ok = false;
                    }
                    else if (value is double?)
                    {
                        if (MinValue != null && (MinValue as DateTime?).Value > (value as DateTime?).Value)
                            ok = false;
                        if (MaxValue != null && (MaxValue as DateTime?).Value < (value as DateTime?).Value)
                            ok = false;
                    }
                    if (ok)
                        _value = value;
                }
            }
            get
            {
                return _value;
            }
        }

        /// <summary>
        /// Last modification date/time
        /// </summary>
        public DateTime? ModifiedDate { set; get; }

        /// <summary>
        /// Permission to change the Value
        /// </summary>
        public bool? Readonly { set; get; }

        private T _minvalue = default(T);
        /// <summary>
        /// Minimum allowed value (for int/double/DateTime typs) or null
        /// </summary>
        public T MinValue
        {
            set
            {
                if (value != null && (value is int? || value is double? || value is DateTime?))
                    _minvalue = value;
            }
            get { return _minvalue; }
        }

        private T _maxvalue = default(T);
        /// <summary>
        /// Maximum allowed value (for int/double/DateTime typs) or null
        /// </summary>
        public T MaxValue
        {
            set
            {
                if (value != null && (value is int? || value is double? || value is DateTime?))
                    _maxvalue = value;
            }
            get { return _maxvalue; }
        }

        /// <summary>
        /// Default nullable value of the settings item
        /// </summary>
        public T Default { set; get; }
    }

    /// <summary>
    /// Class for storing Settings items
    /// </summary>
    [Serializable]
    public class SettingsHashtable : Hashtable
    {
        /// <summary>
        /// Adds a new Settings item
        /// </summary>
        /// <param name="key">Settings item name</param>
        /// <param name="value">Settings item value</param>
        public override void Add(object key, object value)
        {
            base[key] = value;
        }

        /// <summary>
        /// Returns the whole Settings Item object
        /// </summary>
        /// <param name="key">Item key in the hashtable</param>
        /// <param name="argument">Additional internal argument</param>
        /// <returns>Item object</returns>
        public object this[object key, object argument]
        {
            get
            {
                return base[key];
            }
            // not set
            private set
            {
            }
        }

        /// <summary>
        /// Settings items
        /// </summary>
        /// <param name="key">Settings item name</param>
        /// <returns>Settings item value</returns>
        public override object this[object key]
        {
            get
            {
                if (base[key] == null)
                    throw new Exception("Item doesn't exist.");

                if (base[key] is Item<int?>)
                    return ((base[key] as Item<int?>).Value).Value;
                else if (base[key] is Item<string>)
                    return (base[key] as Item<string>).Value;
                else if (base[key] is Item<bool?>)
                    return (base[key] as Item<bool?>).Value;
                else if (base[key] is Item<double?>)
                    return (base[key] as Item<double?>).Value;
                else if (base[key] is Item<Color?>)
                    return (base[key] as Item<Color?>).Value;
                else if (base[key] is Item<DateTime?>)
                    return (base[key] as Item<DateTime?>).Value;
                else if (base[key] is Item<Font>)
                    return (base[key] as Item<Font>).Value;

                return null;
            }
            set
            {
                if (base[key] == null)
                {
                    if (value is int)
                        base[key] = new Item<int?>(key.ToString(), (int)value, null, null, null, null, null, null);
                    else if (value is string)
                        base[key] = new Item<string>(key.ToString(), (string)value, null, null, null, null, null, null);
                    else if (value is bool)
                        base[key] = new Item<bool?>(key.ToString(), (bool)value, null, null, null, null, null, null);
                    else if (value is double)
                        base[key] = new Item<double?>(key.ToString(), (double)value, null, null, null, null, null, null);
                    else if (value is Color)
                        base[key] = new Item<Color?>(key.ToString(), (Color)value, null, null, null, null, null, null);
                    else if (value is DateTime)
                        base[key] = new Item<DateTime?>(key.ToString(), (DateTime)value, null, null, null, null, null, null);
                    else if (value is Font)
                        base[key] = new Item<Font>(key.ToString(), (Font)value, null, null, null, null, null, null);
                }
                else
                {
                    if (base[key] is Item<int?>)
                        ((Item<int?>)base[key]).Value = (int?)value;
                    else if (base[key] is Item<string>)
                        ((Item<string>)base[key]).Value = (string)value;
                    else if (base[key] is Item<bool?>)
                        ((Item<bool?>)base[key]).Value = (bool)value;
                    else if (base[key] is Item<double?>)
                        ((Item<double?>)base[key]).Value = (double)value;
                    else if (base[key] is Item<Color>)
                        ((Item<Color?>)base[key]).Value = (Color)value;
                    else if (base[key] is Item<DateTime>)
                        ((Item<DateTime?>)base[key]).Value = (DateTime)value;
                    else if (base[key] is Item<Font>)
                        (base[key] as Item<Font>).Value = (Font)value;
                }
            }
        }
    }


    /// <summary>
    /// Class for handling and manipulating Settings items
    /// </summary>
    public class Settings4Net
    {
        /// <summary>
        /// Collection of Settings Items
        /// </summary>
        public SettingsHashtable Settings = new SettingsHashtable();

        /// <summary>
        /// Gets or sets an option for tracking date/time of last settings item modification
        /// </summary>
        public bool TrackModificationTimes { get; set; }

        /// <summary>
        /// Gets or sets  an option to perform checksums on settings file to prevent manual editing of the file
        /// </summary>
        public bool PreventManualModifications { get; set; }

        /// <summary>
        /// Indicates if Settings were loaded from the file
        /// </summary>
        public bool IsLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicates if Settings file was modified manually
        /// </summary>
        public bool WasModifiedManually
        {
            get;
            private set;
        }

        /// <summary>
        /// Checksum of the loaded file
        /// </summary>
        private string FileChecksum = "";


        #region InternalClassProperties
        private string _xml_version = "1.0";
        private string _xml_encoding = "utf-8";
        private string _magic_salt = "RG_Setting4Net";
        #endregion


        /// <summary>
        /// Opens and reads settings from specified file
        /// </summary>
        /// <param name="filename">Settings file</param>
        /// <returns>Status of the operation</returns>
        public bool Open(string filename)
        {
            return Open(filename, PreventManualModifications, false); ;
        }


        /// <summary>
        /// Opens and reads settings from specified file
        /// </summary>
        /// <param name="filename">Settings file</param>
        /// <param name="PreventManualModifications">If set to true, prevents loading settings if the file was manually modified</param>
        /// <returns>Status of the operation</returns>
        public bool Open(string filename, bool PreventManualModifications)
        {
            return Open(filename, PreventManualModifications, false);
        }

        /// <summary>
        /// Opens and reads settings from specified file
        /// </summary>
        /// <param name="filename">Settings file</param>
        /// <param name="PreventManualModifications">If true, prevents loading settings if the file was manually modified</param>
        /// <param name="AppendSettings">If true, appends settings from specified file to already existing ones instead of overwriting</param>
        /// <returns></returns>
        public bool Open(string filename, bool PreventManualModifications, bool AppendSettings)
        {
            IsLoaded = false;

            bool res = false;

            if (!AppendSettings)
                Settings.Clear();

            try
            {
                res = ParseXmlFile(filename);
            }
            catch (Exception)
            {
                return false;
            }

            if (res && PreventManualModifications && (CalculateChecksumValue() != FileChecksum))
            {
                Settings.Clear();
                return false;
            }

            this.PreventManualModifications = PreventManualModifications;
            IsLoaded = res;
            return res;
        }

        /// <summary>
        /// Saves all Settings to specified file
        /// </summary>
        /// <param name="filename">Output file</param>
        public void Save(string filename)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.AppendChild(xmldoc.CreateXmlDeclaration(_xml_version, _xml_encoding, null));

            XmlElement xmlRoot = xmldoc.CreateElement("settings");
            xmldoc.AppendChild(xmlRoot);

            string iname, ivalue, itype;
            bool? ireadonly;
            string imin = null, imax = null, idefault = null;

            foreach (string key in Settings.Keys)
            {
                object setting = Settings[key, true];
                SettingsMetaData imeta;
                if (setting is Item<int?>)
                {
                    itype = "int";
                    iname = (setting as Item<int?>).Name;
                    ireadonly = (setting as Item<int?>).Readonly;
                    ivalue = (setting as Item<int?>).Value.ToString();
                    imin = (setting as Item<int?>).MinValue == null ? null : (setting as Item<int?>).MinValue.Value.ToString();
                    imax = (setting as Item<int?>).MaxValue == null ? null : (setting as Item<int?>).MaxValue.Value.ToString();
                    idefault = (setting as Item<int?>).Default == null ? null : (setting as Item<int?>).Default.Value.ToString();
                    imeta = (setting as Item<int?>).MetaData;
                }
                else if (setting is Item<bool?>)
                {
                    itype = "bool";
                    iname = (setting as Item<bool?>).Name;
                    ireadonly = (setting as Item<bool?>).Readonly;
                    ivalue = (setting as Item<bool?>).Value.ToString();
                    idefault = (setting as Item<bool?>).Default == null ? null : (setting as Item<bool?>).Default.ToString();
                    imeta = (setting as Item<bool?>).MetaData;
                }
                else if (setting is Item<double?>)
                {
                    itype = "double";
                    iname = (setting as Item<double?>).Name;
                    ireadonly = (setting as Item<double?>).Readonly;
                    ivalue = (setting as Item<double?>).Value.ToString();
                    imin = (setting as Item<double?>).MinValue == null ? null : (setting as Item<double?>).MinValue.Value.ToString();
                    imax = (setting as Item<double?>).MaxValue == null ? null : (setting as Item<double?>).MaxValue.Value.ToString();
                    idefault = (setting as Item<double?>).Default == null ? null : (setting as Item<double?>).Default.Value.ToString();
                    imeta = (setting as Item<double?>).MetaData;
                }
                else if (setting is Item<Color?>)
                {
                    itype = "color";
                    iname = (setting as Item<Color?>).Name;
                    ireadonly = (setting as Item<Color?>).Readonly;
                    ivalue = TypeDescriptor.GetConverter(typeof(Color)).ConvertToString((setting as Item<Color?>).Value);
                    idefault = TypeDescriptor.GetConverter(typeof(Color)).ConvertToString((setting as Item<Color?>).Default);
                    imeta = (setting as Item<Color?>).MetaData;
                }
                else if (setting is Item<DateTime?>)
                {
                    itype = "datetime";
                    iname = (setting as Item<DateTime?>).Name;
                    ireadonly = (setting as Item<DateTime?>).Readonly;
                    ivalue = (setting as Item<DateTime?>).Value.ToString();
                    imin = (setting as Item<DateTime?>).MinValue == null ? null : (setting as Item<DateTime?>).MinValue.Value.ToString();
                    imax = (setting as Item<DateTime?>).MaxValue == null ? null : (setting as Item<DateTime?>).MaxValue.Value.ToString();
                    idefault = (setting as Item<DateTime?>).Default == null ? null : (setting as Item<DateTime?>).Default.Value.ToString();
                    imeta = (setting as Item<DateTime?>).MetaData;
                }
                else if (setting is Item<Font>)
                {
                    itype = "font";
                    iname = (setting as Item<Font>).Name;
                    ireadonly = (setting as Item<Font>).Readonly;
                    ivalue = TypeDescriptor.GetConverter(typeof(Font)).ConvertToString((setting as Item<Font>).Value);
                    idefault = TypeDescriptor.GetConverter(typeof(Font)).ConvertToString((setting as Item<Font>).Default);
                    imeta = (setting as Item<Font>).MetaData;
                }
                else // string
                {
                    itype = "string";
                    iname = (setting as Item<string>).Name;
                    ireadonly = (setting as Item<string>).Readonly;
                    ivalue = (setting as Item<string>).Value;
                    idefault = (setting as Item<string>).Default == null ? null : (setting as Item<string>).Default;
                    imeta = (setting as Item<string>).MetaData;
                }

                XmlElement element = xmldoc.CreateElement("item");
                element.InnerText = ivalue;
                element.SetAttribute("name", iname);
                element.SetAttribute("type", itype);
                if (ireadonly != null)
                    element.SetAttribute("readonly", ireadonly.Value.ToString());
                if (imin != null)
                    element.SetAttribute("min", imin);
                if (imax != null)
                    element.SetAttribute("max", imax);
                if (idefault != null)
                    element.SetAttribute("default", idefault);
                if (TrackModificationTimes)
                    element.SetAttribute("modified", DateTime.Now.ToString());
                if (imeta != null)
                {
                    if (imeta.Description != null && imeta.Description != String.Empty)
                        element.SetAttribute("description", imeta.Description);
                    if (imeta.Category != null && imeta.Category != String.Empty)
                        element.SetAttribute("category", imeta.Category);
                    if (imeta.Tag != null && imeta.Tag != String.Empty)
                        element.SetAttribute("tag", imeta.Tag);
                    if (imeta.Visible != null && imeta.Tag != String.Empty)
                        element.SetAttribute("tag", imeta.Tag);
                }
                xmlRoot.AppendChild(element);
            }

            // add checksum element
            XmlElement checksumElement = xmldoc.CreateElement("checksum");
            checksumElement.InnerText = CalculateChecksumValue();
            xmlRoot.AppendChild(checksumElement);

            try
            {
                xmldoc.Save(filename);
            }
            catch (Exception)
            {
                throw new Exception("Couldn't save settings to the specified file");
            }
        }


        /// <summary>
        /// Reads XML-Settings file contents
        /// </summary>
        /// <param name="filename">Input file</param>
        /// <returns>Operation status</returns>
        private bool ParseXmlFile(string filename)
        {
            XmlDocument xmldoc = new XmlDocument();

            try
            {
                xmldoc.Load(filename);
            }
            catch (Exception)
            {
                return false;
            }

            foreach (XmlNode node in xmldoc.GetElementsByTagName("item"))
            {
                ParseANode(node);
            }

            XmlNodeList checksumNode = xmldoc.GetElementsByTagName("checksum");
            if (checksumNode.Count != 1)
                return false;

            FileChecksum = checksumNode[0].InnerText;

            return true;
        }


        /// <summary>
        /// Parses and stores a single Settings item from XmlNode
        /// </summary>
        /// <param name="node">Input XML node</param>
        private void ParseANode(XmlNode node)
        {
            string item_value = node.InnerText;
            string item_name = node.Attributes["name"] == null ? null : node.Attributes["name"].Value;
            if (item_name == null || item_name.Length <= 0)
                throw new Exception("Corrupt XML file");

            string item_type = node.Attributes["type"] == null ? String.Empty : node.Attributes["type"].Value;
            string item_default = node.Attributes["default"] == null ? String.Empty : node.Attributes["default"].Value;
            string item_min = node.Attributes["min"] == null ? String.Empty : node.Attributes["min"].Value;
            string item_max = node.Attributes["max"] == null ? String.Empty : node.Attributes["max"].Value;
            bool? item_readonly = null; bool tmp_b;
            if (node.Attributes["readonly"] != null)
                item_readonly = Boolean.TryParse(node.Attributes["readonly"].Value, out tmp_b) ? tmp_b : (bool?)null;
            DateTime? item_modified = null; DateTime tmp_t;
            if (node.Attributes["modified"] != null)
                item_modified = DateTime.TryParse(node.Attributes["modified"].Value, out tmp_t) ? tmp_t : (DateTime?)null;


            string idesc = node.Attributes["description"] == null ? String.Empty : node.Attributes["description"].Value;
            string icat = node.Attributes["category"] == null ? String.Empty : node.Attributes["category"].Value;
            string itag = node.Attributes["tag"] == null ? String.Empty : node.Attributes["tag"].Value;
            bool? item_visible = null; bool tmp_visible;
            if (node.Attributes["visible"] != null)
                item_visible = Boolean.TryParse(node.Attributes["visible"].Value, out tmp_visible) ? tmp_visible : (bool?)null;

            AddSettingsItem(item_type, item_name, item_value, item_modified, item_readonly, item_min, item_max, item_default, new SettingsMetaData(idesc, icat, itag, item_visible));
        }


        /// <summary>
        /// Adds a new Settings item
        /// </summary>
        /// <param name="itype">Type</param>
        /// <param name="iname">Name</param>
        /// <param name="ivalue">Value</param>
        /// <param name="imodified">Modified datetiem</param>
        /// <param name="ireadonly">Readonly</param>
        /// <param name="imax">MaxValue</param>
        /// <param name="imin">MinValue</param>
        /// <param name="idefault">Default</param>
        /// <param name="imeta">Meta data</param>
        private void AddSettingsItem(string itype, string iname, string ivalue, DateTime? imodified, bool? ireadonly, string imax, string imin, string idefault, SettingsMetaData imeta)
        {
            if (Settings.ContainsKey(iname))
                return;

            switch (itype)
            {
                case "int":
                case "integer":
                case "number":
                    int t_int;
                    if (!int.TryParse(ivalue, out t_int))
                        throw new Exception("Corrupt XML file");

                    Settings.Add(iname, new Item<int?>(
                        iname, int.Parse(ivalue), imodified, ireadonly,
                        (int.TryParse(imin, out t_int) == true) ? t_int : (int?)null,
                        (int.TryParse(imax, out t_int) == true) ? t_int : (int?)null,
                        (int.TryParse(idefault, out t_int) == true) ? t_int : (int?)null, imeta));
                    break;

                case "bool":
                case "boolean":
                case "logic":
                    bool t_bool;
                    if (!bool.TryParse(ivalue, out t_bool))
                        throw new Exception("Corrupt XML file");

                    Settings.Add(iname, new Item<bool?>(
                        iname, bool.Parse(ivalue), imodified, ireadonly,
                        (bool.TryParse(imin, out t_bool) == true) ? t_bool : (bool?)null,
                        (bool.TryParse(imax, out t_bool) == true) ? t_bool : (bool?)null,
                        (bool.TryParse(idefault, out t_bool) == true) ? t_bool : (bool?)null, imeta));
                    break;

                case "double":
                case "float":
                case "real":
                    double t_double;
                    if (!double.TryParse(ivalue, out t_double))
                        throw new Exception("Corrupt XML file");

                    Settings.Add(iname, new Item<double?>(
                        iname, double.Parse(ivalue), imodified, ireadonly,
                        (double.TryParse(imin, out t_double) == true) ? t_double : (double?)null,
                        (double.TryParse(imax, out t_double) == true) ? t_double : (double?)null,
                        (double.TryParse(idefault, out t_double) == true) ? t_double : (double?)null, imeta));
                    break;

                case "color":
                case "rgb":
                case "brush":
                    Color color;
                    try
                    {
                        color = (Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(ivalue);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Corrupt XML file");
                    }

                    Color? color_default = null;
                    try
                    {
                        color_default = (Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString(idefault);
                    }
                    catch (Exception)
                    {
                    }

                    Settings.Add(iname, new Item<Color?>(
                        iname, color, imodified, ireadonly,
                        null,
                        null,
                        color_default == null ? (Color?)null : color_default.Value, imeta));
                    break;

                case "datetime":
                case "date":
                case "time":
                    DateTime t_datetime;
                    if (!DateTime.TryParse(ivalue, out t_datetime))
                        throw new Exception("Corrupt XML file");

                    Settings.Add(iname, new Item<DateTime?>(
                        iname, DateTime.Parse(ivalue), imodified, ireadonly,
                        (DateTime.TryParse(imin, out t_datetime) == true) ? t_datetime : (DateTime?)null,
                        (DateTime.TryParse(imax, out t_datetime) == true) ? t_datetime : (DateTime?)null,
                        (DateTime.TryParse(idefault, out t_datetime) == true) ? t_datetime : (DateTime?)null, imeta));
                    break;

                case "font":
                case "style":
                case "fontstyle":
                    Font font;
                    try
                    {
                        font = (Font)TypeDescriptor.GetConverter(typeof(Font)).ConvertFromString(ivalue);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Corrupt XML file");
                    }

                    Font font_default = null;
                    try
                    {
                        font_default = (Font)TypeDescriptor.GetConverter(typeof(Font)).ConvertFromString(idefault);
                    }
                    catch (Exception)
                    {
                    }

                    Settings.Add(iname, new Item<Font>(
                        iname, font, imodified, ireadonly,
                        null, null, font_default, imeta));
                    break;

                default: // string
                    Settings.Add(iname, new Item<string>(
                        iname, ivalue, imodified, ireadonly,
                        imin, imax, idefault, imeta));
                    break;
            }
        }

        /// <summary>
        /// Adds new Settings item
        /// </summary>
        /// <typeparam name="T">Settings item type</typeparam>
        /// <param name="Name">Settings item name</param>
        /// <param name="Value">Settings item value</param>
        /// <param name="Description">Settings item description</param>
        /// <param name="Category">Settings item category</param>
        /// <param name="Tag">Settings item tag</param>
        /// <param name="Readonly">Settings item visibility</param>
        public void Add<T>(string Name, T Value, string Description, string Category, string Tag, bool Readonly)
        {
            if (Name == null || Value == null)
                throw new NullReferenceException();

            Settings[Name] = Value;
            SetReadonlyProperty(Name, Readonly);
            SetMetaData(Name, Description, Category, Tag, null);
        }

        /// <summary>
        /// Adds new Settings item
        /// </summary>
        /// <typeparam name="T">Settings item type</typeparam>
        /// <param name="Name">Settings item name</param>
        /// <param name="Value">Settings item value</param>
        /// <param name="Description">Settings item description</param>
        /// <param name="Category">Settings item category</param>
        /// <param name="Tag">Settings item tag</param>
        public void Add<T>(string Name, T Value, string Description, string Category, string Tag)
        {
            if (Name == null || Value == null)
                throw new NullReferenceException();

            Settings[Name] = Value;
            SetMetaData(Name, Description, Category, Tag, null);
        }

        /// <summary>
        /// Adds new Settings item
        /// </summary>
        /// <typeparam name="T">Settings item type</typeparam>
        /// <param name="Name">Settings item name</param>
        /// <param name="Value">Settings item value</param>
        /// <param name="Description">Settings item description</param>
        public void Add<T>(string Name, T Value, string Description)
        {
            if (Name == null || Value == null)
                throw new NullReferenceException();

            Settings[Name] = Value;
            SetMetaData(Name, Description, null, null, null);
        }


        /// <summary>
        /// Sets default value of a Settings item
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <param name="DefaultValue">Default value</param>
        public void SetDefaultItemValue(string Name, object DefaultValue)
        {
            if (Settings[Name, true] == null)
                throw new NullReferenceException();

            if (Settings[Name, true] is Item<int?>)
                (Settings[Name, true] as Item<int?>).Default = (int?)DefaultValue;
            else if (Settings[Name, true] is Item<bool?>)
                (Settings[Name, true] as Item<bool?>).Default = (bool?)DefaultValue;
            else if (Settings[Name, true] is Item<double?>)
                (Settings[Name, true] as Item<double?>).Default = (double?)DefaultValue;
            else if (Settings[Name, true] is Item<Color?>)
                (Settings[Name, true] as Item<Color?>).Default = (Color?)DefaultValue;
            else if (Settings[Name, true] is Item<DateTime?>)
                (Settings[Name, true] as Item<DateTime?>).Default = (DateTime?)DefaultValue;
            else if (Settings[Name, true] is Item<Font>)
                (Settings[Name, true] as Item<Font>).Default = (Font)DefaultValue;
            else // string
                (Settings[Name, true] as Item<string>).Default = (string)DefaultValue;
        }

        /// <summary>
        /// Sets minimum allowed value of a Settings item
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <param name="MinValue">Minimum value</param>
        public void SetMinItemValue(string Name, object MinValue)
        {
            if (Settings[Name, true] == null)
                throw new NullReferenceException();

            if (Settings[Name, true] is Item<int?>)
                (Settings[Name, true] as Item<int?>).MinValue = (int?)MinValue;
            else if (Settings[Name, true] is Item<double?>)
                (Settings[Name, true] as Item<double?>).MinValue = (double?)MinValue;
            else if (Settings[Name, true] is Item<DateTime?>)
                (Settings[Name, true] as Item<DateTime?>).MinValue = (DateTime?)MinValue;
        }


        /// <summary>
        /// Sets maximum allowed value of a Settings item
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <param name="MaxValue">Maximum value</param>
        public void SetMaxItemValue(string Name, object MaxValue)
        {
            if (Settings[Name, true] == null)
                throw new NullReferenceException();

            if (Settings[Name, true] is Item<int?>)
                (Settings[Name, true] as Item<int?>).MaxValue = (int?)MaxValue;
            else if (Settings[Name, true] is Item<double?>)
                (Settings[Name, true] as Item<double?>).MaxValue = (double?)MaxValue;
            else if (Settings[Name, true] is Item<DateTime?>)
                (Settings[Name, true] as Item<DateTime?>).MaxValue = (DateTime?)MaxValue;
        }


        /// <summary>
        /// Sets readonly propery of Settings item, enabling/disabling its modifications
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <param name="Readonly">true - allows changes; false - prevents changes untill readonly value is changed</param>
        public void SetReadonlyProperty(string Name, bool? Readonly)
        {
            if (Settings[Name, true] == null)
                throw new NullReferenceException();

            if (Settings[Name, true] is Item<int?>)
                (Settings[Name, true] as Item<int?>).Readonly = Readonly;
            else if (Settings[Name, true] is Item<bool?>)
                (Settings[Name, true] as Item<bool?>).Readonly = Readonly;
            else if (Settings[Name, true] is Item<double?>)
                (Settings[Name, true] as Item<double?>).Readonly = Readonly;
            else if (Settings[Name, true] is Item<Color?>)
                (Settings[Name, true] as Item<Color?>).Readonly = Readonly;
            else if (Settings[Name, true] is Item<DateTime?>)
                (Settings[Name, true] as Item<DateTime?>).Readonly = Readonly;
            else if (Settings[Name, true] is Item<Font>)
                (Settings[Name, true] as Item<Font>).Readonly = Readonly;
            else // string
                (Settings[Name, true] as Item<string>).Readonly = Readonly;
        }

        /// <summary>
        /// Sets metadata of specified settings item
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <param name="Description">Description (if null = do not set)</param>
        /// <param name="Category">Category (if null = do not set)</param>
        /// <param name="Tag">Tag (if null = do not set)</param>
        /// <param name="Visible">Visibility</param>
        private void SetMetaData(string Name, string Description, string Category, string Tag, bool? Visible)
        {
            if (Settings[Name, true] == null)
                throw new NullReferenceException();

            if (Settings[Name, true] is Item<int?>)
            {
                if (Description != null)
                    (Settings[Name, true] as Item<int?>).MetaData.Description = Description;
                if (Category != null)
                    (Settings[Name, true] as Item<int?>).MetaData.Category = Category;
                if (Tag != null)
                    (Settings[Name, true] as Item<int?>).MetaData.Tag = Tag;
                if (Visible != null)
                    (Settings[Name, true] as Item<int?>).MetaData.Visible = Visible.Value;
            }
            else if (Settings[Name, true] is Item<bool?>)
            {
                if (Description != null)
                    (Settings[Name, true] as Item<bool?>).MetaData.Description = Description;
                if (Category != null)
                    (Settings[Name, true] as Item<bool?>).MetaData.Category = Category;
                if (Tag != null)
                    (Settings[Name, true] as Item<bool?>).MetaData.Tag = Tag;
                if (Visible != null)
                    (Settings[Name, true] as Item<bool?>).MetaData.Visible = Visible.Value;
            }
            else if (Settings[Name, true] is Item<double?>)
            {
                if (Description != null)
                    (Settings[Name, true] as Item<double?>).MetaData.Description = Description;
                if (Category != null)
                    (Settings[Name, true] as Item<double?>).MetaData.Category = Category;
                if (Tag != null)
                    (Settings[Name, true] as Item<double?>).MetaData.Tag = Tag;
                if (Visible != null)
                    (Settings[Name, true] as Item<double?>).MetaData.Visible = Visible.Value;
            }
            else if (Settings[Name, true] is Item<Color?>)
            {
                if (Description != null)
                    (Settings[Name, true] as Item<Color?>).MetaData.Description = Description;
                if (Category != null)
                    (Settings[Name, true] as Item<Color?>).MetaData.Category = Category;
                if (Tag != null)
                    (Settings[Name, true] as Item<Color?>).MetaData.Tag = Tag;
                if (Visible != null)
                    (Settings[Name, true] as Item<Color?>).MetaData.Visible = Visible.Value;
            }
            else if (Settings[Name, true] is Item<DateTime?>)
            {
                if (Description != null)
                    (Settings[Name, true] as Item<DateTime?>).MetaData.Description = Description;
                if (Category != null)
                    (Settings[Name, true] as Item<DateTime?>).MetaData.Category = Category;
                if (Tag != null)
                    (Settings[Name, true] as Item<DateTime?>).MetaData.Tag = Tag;
                if (Visible != null)
                    (Settings[Name, true] as Item<DateTime?>).MetaData.Visible = Visible.Value;
            }
            else if (Settings[Name, true] is Item<Font>)
            {
                if (Description != null)
                    (Settings[Name, true] as Item<Font>).MetaData.Description = Description;
                if (Category != null)
                    (Settings[Name, true] as Item<Font>).MetaData.Category = Category;
                if (Tag != null)
                    (Settings[Name, true] as Item<Font>).MetaData.Tag = Tag;
                if (Visible != null)
                    (Settings[Name, true] as Item<Font>).MetaData.Visible = Visible.Value;
            }
            else // string
            {
                if (Description != null)
                    (Settings[Name, true] as Item<string>).MetaData.Description = Description;
                if (Category != null)
                    (Settings[Name, true] as Item<string>).MetaData.Category = Category;
                if (Tag != null)
                    (Settings[Name, true] as Item<string>).MetaData.Tag = Tag;
                if (Visible != null)
                    (Settings[Name, true] as Item<string>).MetaData.Visible = Visible.Value;
            }
        }

        /// <summary>
        /// Sets Settings item description
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <param name="Description">Description</param>
        public void SetItemDescription(string Name, string Description)
        {
            SetMetaData(Name, Description, null, null, null);
        }

        /// <summary>
        /// Sets Settings item category
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <param name="Category">Category</param>
        public void SetItemCategory(string Name, string Category)
        {
            SetMetaData(Name, null, Category, null, null);
        }

        /// <summary>
        /// Sets Settings item freely used tag
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <param name="Tag">Tag value</param>
        public void SetItemTag(string Name, string Tag)
        {
            SetMetaData(Name, null, null, Tag, null);
        }

        /// <summary>
        /// Sets Settings item visibility
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <param name="Visible">Visibility</param>
        public void SetItemVisibility(string Name, bool Visible)
        {
            SetMetaData(Name, null, null, null, Visible);
        }

        /// <summary>
        /// Gets Settings item meta data object
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <returns>SettingsMetaData object containing all meta data information</returns>
        public SettingsMetaData GetMetaData(string Name)
        {
            if (Settings[Name, true] == null)
                throw new NullReferenceException();

            if (Settings[Name, true] is Item<int?>)
                return (Settings[Name, true] as Item<int?>).MetaData;
            else if (Settings[Name, true] is Item<bool?>)
                return (Settings[Name, true] as Item<bool?>).MetaData;
            else if (Settings[Name, true] is Item<double?>)
                return (Settings[Name, true] as Item<double?>).MetaData;
            else if (Settings[Name, true] is Item<Color?>)
                return (Settings[Name, true] as Item<Color?>).MetaData;
            else if (Settings[Name, true] is Item<DateTime?>)
                return (Settings[Name, true] as Item<DateTime?>).MetaData;
            else if (Settings[Name, true] is Item<Font>)
                return (Settings[Name, true] as Item<Font>).MetaData;
            // string
            return (Settings[Name, true] as Item<string>).MetaData;
        }

        /// <summary>
        /// Gets Settings item description
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <returns>Description</returns>
        public string GetItemDescription(string Name)
        {
            return GetMetaData(Name).Description;
        }

        /// <summary>
        /// Gets Settings item category
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <returns>Category</returns>
        public string GetItemCategory(string Name)
        {
            return GetMetaData(Name).Category;
        }

        /// <summary>
        /// Gets Settings item tag
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <returns>Tag value</returns>
        public string GetItemTag(string Name)
        {
            return GetMetaData(Name).Tag;
        }

        /// <summary>
        /// Gets Settings item visibility
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <returns>Visible value</returns>
        public bool GetItemVisibility(string Name)
        {
            return (GetMetaData(Name).Visible == null) ? true : GetMetaData(Name).Visible.Value;
        }

        /// <summary>
        /// Gets default value of a Settings Item
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <returns>Default value</returns>
        public object GetDefaultItemValue(string Name)
        {
            if (Settings[Name, true] == null)
                throw new NullReferenceException();

            if (Settings[Name, true] is Item<int?>)
                return (Settings[Name, true] as Item<int?>).Default;
            else if (Settings[Name, true] is Item<bool?>)
                return (Settings[Name, true] as Item<bool?>).Default;
            else if (Settings[Name, true] is Item<double?>)
                return (Settings[Name, true] as Item<double?>).Default;
            else if (Settings[Name, true] is Item<Color?>)
                return (Settings[Name, true] as Item<Color?>).Default;
            else if (Settings[Name, true] is Item<DateTime?>)
                return (Settings[Name, true] as Item<DateTime?>).Default;
            else if (Settings[Name, true] is Item<Font>)
                return (Settings[Name, true] as Item<Font>).Default;
            // string
            return (Settings[Name, true] as Item<string>).Default;
        }


        /// <summary>
        /// Gets Settings item value if it exists, or Default one
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <param name="Default">If settings item doesn't exist - return this value</param>
        /// <returns></returns>
        public object GetItemOrDefaultValue(string Name, object Default)
        {
            if (Settings[Name, true] == null)
                return Default;

            return Settings[Name];
        }

        /// <summary>
        /// Gets minimum allowed value of a Settings Item
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <returns>Minimum value</returns>
        public object GetMinItemValue(string Name)
        {
            if (Settings[Name, true] == null)
                throw new NullReferenceException();

            if (Settings[Name, true] is Item<int?>)
                return (Settings[Name, true] as Item<int?>).MinValue;
            else if (Settings[Name, true] is Item<double?>)
                return (Settings[Name, true] as Item<double?>).MinValue;
            else if (Settings[Name, true] is Item<DateTime?>)
                return (Settings[Name, true] as Item<DateTime?>).MinValue;

            return null;
        }

        /// <summary>
        /// Gets maximum allowed value of a Settings Item
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <returns>Maximum value</returns>
        public object GetMaxItemValue(string Name)
        {
            if (Settings[Name, true] == null)
                throw new NullReferenceException();

            if (Settings[Name, true] is Item<int?>)
                return (Settings[Name, true] as Item<int?>).MaxValue;
            else if (Settings[Name, true] is Item<double?>)
                return (Settings[Name, true] as Item<double?>).MaxValue;
            else if (Settings[Name, true] is Item<DateTime?>)
                return (Settings[Name, true] as Item<DateTime?>).MaxValue;

            return null;
        }

        /// <summary>
        /// Gets readonly property of a Settings item
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <returns>Readonly value</returns>
        public bool GetReadonlyProperty(string Name)
        {
            if (Settings[Name, true] == null)
                throw new NullReferenceException();

            bool? result = null;

            if (Settings[Name, true] is Item<int?>)
                result = (Settings[Name, true] as Item<int?>).Readonly;
            else if (Settings[Name, true] is Item<bool?>)
                result = (Settings[Name, true] as Item<bool?>).Readonly;
            else if (Settings[Name, true] is Item<double?>)
                result = (Settings[Name, true] as Item<double?>).Readonly;
            else if (Settings[Name, true] is Item<Color?>)
                result = (Settings[Name, true] as Item<Color?>).Readonly;
            else if (Settings[Name, true] is Item<DateTime?>)
                result = (Settings[Name, true] as Item<DateTime?>).Readonly;
            else if (Settings[Name, true] is Font)
                result = (Settings[Name, true] as Item<Font>).Readonly;
            else // string
                result = (Settings[Name, true] as Item<string>).Readonly;

            if (result != null && result.Value == true)
                return true;
            return false;
        }


        /// <summary>
        /// Resets Settings item value to default or specified one 
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <param name="DefaultValue">Default value (used if Settings item doesn't have a specified one)</param>
        public void ResetToDefaultValue(string Name, object DefaultValue)
        {
            if (Settings[Name, true] == null)
                throw new NullReferenceException();

            if (Settings[Name, true] is Item<int?>)
                (Settings[Name, true] as Item<int?>).Value = (Settings[Name, true] as Item<int?>).Default ?? (int?)DefaultValue ?? (Settings[Name, true] as Item<int?>).Value;
            else if (Settings[Name, true] is Item<bool?>)
                (Settings[Name, true] as Item<bool?>).Value = (Settings[Name, true] as Item<bool?>).Default ?? (bool?)DefaultValue ?? (Settings[Name, true] as Item<bool?>).Value;
            else if (Settings[Name, true] is Item<double?>)
                (Settings[Name, true] as Item<double?>).Value = (Settings[Name, true] as Item<double?>).Default ?? (double?)DefaultValue ?? (Settings[Name, true] as Item<double?>).Value;
            else if (Settings[Name, true] is Item<Color?>)
                (Settings[Name, true] as Item<Color?>).Value = (Settings[Name, true] as Item<Color?>).Default ?? (Color?)DefaultValue ?? (Settings[Name, true] as Item<Color?>).Value;
            else if (Settings[Name, true] is Item<DateTime?>)
                (Settings[Name, true] as Item<DateTime?>).Value = (Settings[Name, true] as Item<DateTime?>).Default ?? (DateTime?)DefaultValue ?? (Settings[Name, true] as Item<DateTime?>).Value;
            else if (Settings[Name, true] is Item<Font>)
                (Settings[Name, true] as Item<Font>).Value = (Settings[Name, true] as Item<Font>).Default ?? (Font)DefaultValue ?? (Settings[Name, true] as Item<Font>).Value;
            else // string
                (Settings[Name, true] as Item<string>).Value = (Settings[Name, true] as Item<string>).Default ?? (string)DefaultValue ?? (Settings[Name, true] as Item<string>).Value;
        }


        /// <summary>
        /// If setting file is keeping track of modification dates/times, returns the last DateTime of such event
        /// </summary>
        /// <param name="Name">Settings item name</param>
        /// <returns>DateTime object of the last modification event or an empty one if no such event was recorded</returns>
        public DateTime GetModificationDateTime(string Name)
        {
            if (Settings[Name, true] == null)
                throw new NullReferenceException();

            if (Settings[Name, true] is Item<int?>)
                return (Settings[Name, true] as Item<int?>).ModifiedDate ?? new DateTime();
            else if (Settings[Name, true] is Item<bool?>)
                return (Settings[Name, true] as Item<bool?>).ModifiedDate ?? new DateTime();
            else if (Settings[Name, true] is Item<double?>)
                return (Settings[Name, true] as Item<double?>).ModifiedDate ?? new DateTime();
            else if (Settings[Name, true] is Item<Color?>)
                return (Settings[Name, true] as Item<Color?>).ModifiedDate ?? new DateTime();
            else if (Settings[Name, true] is Item<DateTime?>)
                return (Settings[Name, true] as Item<DateTime?>).ModifiedDate ?? new DateTime();
            else if (Settings[Name, true] is Item<Font>)
                return (Settings[Name, true] as Item<Font>).ModifiedDate ?? new DateTime();
            else // string
                return (Settings[Name, true] as Item<string>).ModifiedDate ?? new DateTime();
        }

        /// <summary>
        /// Resets Settings item value to default one
        /// </summary>
        /// <param name="Name">Settings item name</param>
        public void ResetToDefaultValue(string Name)
        {
            ResetToDefaultValue(Name, null);
        }


        public void ResetAllToDefault()
        {
            foreach (string key in Settings.Keys)
                ResetToDefaultValue(key, null);
        }


        /// <summary>
        /// Returns a partial hash token of a Settings item
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="item">Settings item</param>
        /// <returns>Partial hash</returns>
        private string ComputePartialHash<T>(Item<T> item)
        {
            string hash = "";
            hash += ":name=" + item.Name;
            hash += ":value=" + item.Value;
            hash += ":ro=" + ((item.Readonly != null) ? item.Readonly.Value.ToString() : "?");
            //hash += ":min=" + ((item.MinValue != null) ? item.MinValue.ToString() : "?");
            //hash += ":max=" + ((item.MaxValue != null) ? item.MaxValue.ToString() : "?");
            //hash += ":default=" + ((item.Default != null) ? item.Default.ToString() : "?");
            //hash += ":date=" + ((item.ModifiedDate != null) ? item.ModifiedDate.Value.ToString() : "?");
            return hash;
        }


        /// <summary>
        /// Calculates the checksum/hash value of all Settings items
        /// </summary>
        /// <returns>checksum/hash value</returns>
        public string CalculateChecksumValue()
        {
            string part = "";
            ArrayList list = new ArrayList(Settings.Count);
            foreach (string key in Settings.Keys)
            {
                object setting = Settings[key, true];
                if (setting is Item<int?>)
                    part = ComputePartialHash<int?>(setting as Item<int?>);
                else if (setting is Item<bool?>)
                    part = ComputePartialHash<bool?>(setting as Item<bool?>);
                else if (setting is Item<double?>)
                    part = ComputePartialHash<double?>(setting as Item<double?>);
                else if (setting is Item<Color?>)
                    part = ComputePartialHash<Color?>(setting as Item<Color?>);
                else if (setting is Item<DateTime?>)
                    part = ComputePartialHash<DateTime?>(setting as Item<DateTime?>);
                //else if (setting is Item<Font>)
                //;// todo: part = ComputePartialHash<Font>(setting as Item<Font>);
                else
                    part = ComputePartialHash<string>(setting as Item<string>);

                list.Add(part);
            }

            // we need a list, to ensure that the order of the elements remains the same regardless of load-order
            list.Sort();
            StringBuilder sb = new StringBuilder("");
            foreach (string s in list)
                sb.Append(s);

            sb.Append(_magic_salt);

            ulong hash = 0;
            for (ulong i = 0; i < (ulong)sb.Length; i++)
                hash += i * (ulong)sb.Length + (ulong)Convert.ToInt64(sb[(int)i]);

            // adding some magic salt
            hash = long.MaxValue ^ hash + 08 + 20 + 1985;

            // mixing the values
            hash = (~hash) + (hash << 21);
            hash = hash ^ (hash >> 24);
            hash = (hash + (hash << 3)) + (hash << 8);
            hash = hash ^ (hash >> 14);
            hash = (hash + (hash << 2)) + (hash << 4);
            hash = hash ^ (hash >> 28);
            hash = hash + (hash << 31);

            return hash.ToString();
        }


        /// <summary>
        /// Generates and returns PropertyGrid-readable object that could be used as PropertyGrid.SelectedObject
        /// </summary>
        /// <returns>PropertyGrid-readable object</returns>
        public object GeneratePropertyGridObject()
        {
            PropertyContainer container = new PropertyContainer();
            Property.Settings = Settings;
            foreach (string key in Settings.Keys)
            {
                object setting = Settings[key, true];
                if (setting is Item<int?>)
                    container.AddProperty(new Property(key, Type.GetType("System.Int32"), (setting as Item<int?>).Readonly == null ? false : (setting as Item<int?>).Readonly.Value,
                        (setting as Item<int?>).MetaData.Description, (setting as Item<int?>).MetaData.Category, (setting as Item<int?>).MetaData.Visible));
                else if (setting is Item<bool?>)
                    container.AddProperty(new Property(key, Type.GetType("System.Boolean"), (setting as Item<bool?>).Readonly == null ? false : (setting as Item<bool?>).Readonly.Value,
                        (setting as Item<bool?>).MetaData.Description, (setting as Item<bool?>).MetaData.Category, (setting as Item<bool?>).MetaData.Visible));
                else if (setting is Item<double?>)
                    container.AddProperty(new Property(key, Type.GetType("System.Double"), (setting as Item<double?>).Readonly == null ? false : (setting as Item<double?>).Readonly.Value,
                        (setting as Item<double?>).MetaData.Description, (setting as Item<double?>).MetaData.Category, (setting as Item<double?>).MetaData.Visible));
                else if (setting is Item<Color?>)
                    container.AddProperty(new Property(key, typeof(System.Drawing.Color), (setting as Item<Color?>).Readonly == null ? false : (setting as Item<Color?>).Readonly.Value,
                        (setting as Item<Color?>).MetaData.Description, (setting as Item<Color?>).MetaData.Category, (setting as Item<Color?>).MetaData.Visible));
                else if (setting is Item<DateTime?>)
                    container.AddProperty(new Property(key, Type.GetType("System.DateTime"), (setting as Item<DateTime?>).Readonly == null ? false : (setting as Item<DateTime?>).Readonly.Value,
                        (setting as Item<DateTime?>).MetaData.Description, (setting as Item<DateTime?>).MetaData.Category, (setting as Item<DateTime?>).MetaData.Visible));
                else if (setting is Item<Font>)
                    container.AddProperty(new Property(key, typeof(System.Drawing.Font), (setting as Item<Font>).Readonly == null ? false : (setting as Item<Font>).Readonly.Value,
                        (setting as Item<Font>).MetaData.Description, (setting as Item<Font>).MetaData.Category, (setting as Item<Font>).MetaData.Visible));
                else // string
                    container.AddProperty(new Property(key, Type.GetType("System.String"), (setting as Item<string>).Readonly == null ? false : (setting as Item<string>).Readonly.Value,
                        (setting as Item<string>).MetaData.Description, (setting as Item<string>).MetaData.Category, (setting as Item<string>).MetaData.Visible));
                container[key] = Settings[key];
            }

            return container;
        }
    }

    #endregion

}
