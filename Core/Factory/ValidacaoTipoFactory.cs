namespace Core.Factory
{
    public class ValidacaoTipoFactory : BaseFactory
    {
        public T CriarValidacoTipo<T, TEnum>(TEnum Tipo)
        {
            return Construir<T, TEnum>(
                Tipo,
                "Core.ValidacoesDeTipo.",
                MontarArgs(Tipo));
        }
    }
}