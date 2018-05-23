using System;
using System.Collections.Generic;

namespace Core.Factory {

    public abstract class BaseFactory
    {
         public T Construir<T, TEnum>(TEnum MeuEnum, string Namespace, object[] Args)
        {
            var nomeDoTipo = ObterNomeDoTipo(MeuEnum, Namespace);
            object[] args = MontarArgs(MeuEnum);
            Type type = ObterTipo(nomeDoTipo);
            return GerarInstancia<T>(nomeDoTipo, args, type);
        }

        private static T GerarInstancia<T>(string nomeDoTipo, object[] args, Type type)
            => (T)type.Assembly
                .CreateInstance(
                    nomeDoTipo,
                    true,
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public,
                    null,
                    args,
                    null,
                    null);

        public static object[] MontarArgs<TEnum>(TEnum TipoEnum)
            => new List<object>().ToArray();

        private static Type ObterTipo(string nomeDoTipo)
            => Type.GetType(nomeDoTipo);

        private static string ObterNomeDoTipo<TEnum>(TEnum TipoEnum, string Namespace)
            => string.Concat(Namespace, TipoEnum.ToString());
    }

}