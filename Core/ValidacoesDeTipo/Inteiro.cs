namespace Core.ValidacoesDeTipo
{
    public class Inteiro : IBaseValidacao
    {
        public Inteiro()
        {
            
        }

        public bool Validar(string valor)
            => valor == null || int.TryParse(valor, out int numero);
    }
}