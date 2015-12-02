using Microsoft.Extensions.Configuration;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using System.Reflection;
using System.Linq;

namespace Sample.CsConfig
{
    public class CsConfigurationProvider
        : ConfigurationProvider
    {
        string _filePath;
        Encoding _fileEncoding;
        ///<summary>
        ///  <params>
        ///  <params>
        ///</summary>
        public CsConfigurationProvider(string configFilePath, Encoding fileEncoding = null)
        {
            _filePath = configFilePath;
            if (fileEncoding != null)
            {
                _fileEncoding = fileEncoding;
            }
            else
            {
                _fileEncoding = Encoding.UTF8;
            }
        }
        ///<summary>get dictionary from any object</summary>
        IDictionary<string, string> GetDictionaryByReflection(string prefix, object obj)
        {
            var t = obj.GetType();
            // other ConfigurationProvider also use StringComparer.OrdinalIgnoreCase,so I use it
            var ret = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var p in t.GetProperties().Where(x => x.CanRead))
            {
                var val = p.GetValue(obj);
                if(val == null)
                {
                    // if value is null,should not process
                    continue;
                }
                ret[$"{prefix}{p.Name}"] = val.ToString();
                if (p.PropertyType != typeof(string))
                {
                    // get child section with recursive
                    foreach (var kv in GetDictionaryByReflection(prefix + p.Name + ":", val))
                    {
                        ret[kv.Key] = kv.Value;
                    }
                }
            }
            return ret;
        }
        public override void Load()
        {
            var code = File.ReadAllText(_filePath, _fileEncoding);
            var opts = ScriptOptions.Default
                .AddReferences(typeof(Microsoft.Extensions.Configuration.Constants).GetTypeInfo().Assembly)
                ;
            var state = CSharpScript.RunAsync<IDictionary<string, string>>(code, options: opts)
                .Result
                ;
            var scriptRet = state.GetVariable("ReturnValue");
            var res = scriptRet == null ? null : scriptRet.Value;
            if (res != null)
            {
                foreach (var kv in GetDictionaryByReflection("", res))
                {
                    Data[kv.Key] = kv.Value;
                }
            }
        }
    }
}