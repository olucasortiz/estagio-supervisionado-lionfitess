namespace LionFitness.Application.Exceptions
{
    public class DuplicateCpfException : Exception
    {
        public DuplicateCpfException(string cpf) : base($"A member with CPF '{cpf}' already exists.")
        {
        }
    }
}
