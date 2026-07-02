using AIPlanningLab.Domain.Models;
using AIPlanningLab.Domain.Registry;
using AIPlanningLab.Infrastructure.Parser;
using System.Text.RegularExpressions;

namespace AIPlanningLab.Implementations.Explicit.Parser;

public sealed class ExplicitProblemParser : IProblemParser
{
    public IPlanningProblem Parse(string path)
    {
        string text = File.ReadAllText(path);

        // propositions
        var registry = new PropositionRegistry();
        foreach (var name in ParseList(ReadBlock(text, "predicates")))
            registry.Register(name);

        // states
        var initial = ExplicitState.FromPropositions(
            registry, PositiveOnly(ParseList(ReadBlock(text, "initial"))));

        var goal = ExplicitState.FromPropositions(
            registry, PositiveOnly(ParseList(ReadBlock(text, "goal"))));

        // constraints
        var constraintsBlock = ReadBlock(text, "constraints");
        var constraints = string.IsNullOrWhiteSpace(constraintsBlock)
            ? ExplicitState.Empty(registry)
            : ExplicitState.FromPropositions(registry, PositiveOnly(ParseList(constraintsBlock)));

        // actions
        var actions = ParseActions(ReadBlock(text, "actionsSet"), registry);

        var domain = new ExplicitDomain(registry, actions.ToList());
        return new ExplicitPlanningProblem(domain, initial, goal, constraints);
    }


    private IReadOnlyCollection<IAction> ParseActions(string text, IPropositionRegistry registry)
    {
        var actions = new List<IAction>();

        var matches = Regex.Matches(text, @"<action>(.*?)<\\action>", RegexOptions.Singleline);

        foreach (Match actionBlock in matches)
        {
            string block = actionBlock.Groups[1].Value;

            string name = ReadInline(block, "name").Trim();

            var pre = ExplicitState.FromPropositions(
                registry, PositiveOnly(ParseList(ReadInline(block, "pre"))));

            SplitEffects(ParseList(ReadInline(block, "pos")), registry, out var add, out var del);

            actions.Add(new ExplicitAction(name, pre, add, del));
        }

        return actions;
    }

    private void SplitEffects(
        IEnumerable<string> tokens,
        IPropositionRegistry registry,
        out ExplicitState add,
        out ExplicitState delete)
    {
        var addNames = new List<string>();
        var delNames = new List<string>();

        foreach (var token in tokens)
        {
            if (token.StartsWith('~'))
                delNames.Add(token[1..]);
            else
                addNames.Add(token);
        }

        add = ExplicitState.FromPropositions(registry, addNames);
        delete = ExplicitState.FromPropositions(registry, delNames);
    }

    private static IEnumerable<string> PositiveOnly(IEnumerable<string> tokens)
        => tokens.Where(t => !t.StartsWith('~'));

    private string ReadBlock(string text, string tag)
    {
        var match = Regex.Match(text, $@"<{tag}>\s*(.*?)\s*<\\{tag}>", RegexOptions.Singleline);
        return match.Success ? match.Groups[1].Value : "";
    }

    private string ReadInline(string text, string tag) => ReadBlock(text, tag);

    private IReadOnlyCollection<string> ParseList(string input)
        => input.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => x.Length > 0)
                .ToArray();
}