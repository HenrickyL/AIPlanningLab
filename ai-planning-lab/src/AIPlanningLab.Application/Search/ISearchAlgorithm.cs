namespace AIPlanningLab.Application.Search;

/// <summary>
/// Executa busca sobre um espaço de estados.
/// Não conhece planejamento.
/// </summary>
public interface ISearchAlgorithm
{
    SearchResult Search(SearchNode root);
}
