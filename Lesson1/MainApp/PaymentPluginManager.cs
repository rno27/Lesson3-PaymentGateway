using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Abstractions;


namespace Lesson3.UI
{
    public class PaymentPluginManager
    {        

        public PaymentPluginManager()
        {
            Plugins = new List<PaymentPlugin>();
        }
        public List<PaymentPlugin> Plugins { get; protected set; }

        public void LoadPlugins()
        {
                        
            var executableLocation = Assembly.GetEntryAssembly().Location;
            var path = Path.Combine(Path.GetDirectoryName(executableLocation), "Plugins");
            var assemblies = Directory
                        .GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly)
                        .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                        .ToList();
            
           foreach(var assembly in assemblies)
           {
               var pluginInstances = LoadPluginsFromAssembly(assembly);
               Plugins.AddRange(pluginInstances);               
           }            
        }

        private IEnumerable<PaymentPlugin> LoadPluginsFromAssembly(Assembly assemblyToScan)
        {
            var currentList = new List<PaymentPlugin>();
            var exportedTypes = assemblyToScan.ExportedTypes;            
            var interfaceType = typeof(PaymentPlugin);

            foreach(var type in exportedTypes)
            {
                if (interfaceType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                {
                   var pluginInstance = (PaymentPlugin)Activator.CreateInstance(type);
                   currentList.Add(pluginInstance);
                }
            }

            return currentList;
        }

        
    }

}