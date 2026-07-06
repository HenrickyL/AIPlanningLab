namespace AIPlanningLab.Implementations.Collections.BDD;

public interface IBddManager
{
    /// <summary>
    /// Retorna a constante verdadeira.
    /// </summary>
    IBddSet True();

    /// <summary>
    /// Retorna a constante falsa.
    /// </summary>
    IBddSet False();

    /// <summary>
    /// Cria uma nova variável booleana.
    /// </summary>
    IBddSet CreateVariable();

    /// <summary>
    /// Conjunção lógica.
    /// </summary>
    IBddSet And(
        IBddSet left,
        IBddSet right);

    /// <summary>
    /// Disjunção lógica.
    /// </summary>
    IBddSet Or(
        IBddSet left,
        IBddSet right);

    /// <summary>
    /// Negação lógica.
    /// </summary>
    IBddSet Not(
        IBddSet value);

    /// <summary>
    /// Quantificação existencial.
    /// </summary>
    IBddSet Exists(
        IBddSet formula,
        IEnumerable<int> variables);

    /// <summary>
    /// Verifica se representa a função falsa.
    /// </summary>
    bool IsEmpty(
        IBddSet formula);

    /// <summary>
    /// Verifica equivalência estrutural.
    /// </summary>
    bool Equals(
        IBddSet left,
        IBddSet right);
}