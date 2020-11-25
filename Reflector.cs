using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Collections;

namespace Lab12
{
    static class Reflector
    {
        static public string GetAssemblyName(Type classType)
        {
            
            return Assembly.GetAssembly(classType).GetName().Name;
        }
        static public bool ContainsPublicConstructors(Type classType)
        {
            return classType.GetConstructors().Length == 0 ? false : true;
        }

        static public IEnumerable<string> GetConstructors(Type classType)
        {
            return classType.GetConstructors().Select(x => x.ToString()); 
        }
        static public IEnumerable<string> GetPropsFields(Type classType)
        {
            return classType.GetTypeInfo().DeclaredFields.Select(x => x.Name);
        }

        static public IEnumerable<string> GetInterfaces(Type classType)
        {
            return classType.GetTypeInfo().ImplementedInterfaces.Select(x => x.Name);
        }
            
        static public IEnumerable<string> GetMethodsWithParam(Type classType, Type methodType)
        {
           //debug var x = classType.GetTypeInfo().DeclaredMethods.Select(x => x.GetParameters().Select(y => y.ParameterType).ToArray()).Where(y => y.Contains(methodType)).ToArray();
            return classType.GetTypeInfo().DeclaredMethods.Where(x => x.GetParameters().Select(y => y.ParameterType).Contains(methodType)).Select(x => x.Name);
        }

        //Добавить Invoke
        static public string toFile(Type classType, Type methodType = null, string fileName = "info.txt")
        {
            string rez = "";
            rez += "Assembly Name: " + GetAssemblyName(classType) + "\n";
            rez += "Contains public constructors: " + (ContainsPublicConstructors(classType) ? "true" : "false") + "\n";
            if (ContainsPublicConstructors(classType))
            {
                rez += "Public constructors:\n";
                foreach(string word in GetConstructors(classType))
                {
                    rez += "\t" + word + "\n";
                }
            }

            rez += "Fields and properties:\n";
            foreach (string word in GetPropsFields(classType))
            {
                rez += "\t" + word + "\n";
            }

            rez += "Implemented interfaces:\n";
            foreach (string word in GetInterfaces(classType))
            {
                rez += "\t" + word + "\n";
            }

            if (methodType != null)
            {
                rez += $"Methods with {methodType} param:\n";
                foreach (string word in GetMethodsWithParam(classType, methodType))
                {
                    rez += "\t" + word + "\n";
                }
            }

            File.WriteAllText(fileName, rez);

            return rez;
        }

        static public object Create(Type classType, params object[] paramList)
        {
            object obj = null;

            obj = Activator.CreateInstance(classType, paramList);


            return obj;
        }
    }
}
