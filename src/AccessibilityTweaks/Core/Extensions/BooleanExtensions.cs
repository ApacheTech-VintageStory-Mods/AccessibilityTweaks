namespace ApacheTech.VintageMods.AccessibilityTweaks.Core.Extensions;

/// <summary>
///     Extension methods to aid with performing actions based on the state of a boolean value.
/// </summary>
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public static class BooleanExtensions
{
    /// <summary>
    ///     Performs a <see cref="Action"/> if the input boolean value is true.
    /// </summary>
    /// <param name="state">The boolean value that this extension method was called on.</param>
    /// <param name="trueAction">The action to perform, if <paramref name="state"/> equates to <c>true</c>.</param>
    /// <returns>The same boolean value as was passed into the method, as <paramref name="state"/></returns>
    public static bool ActIfTrue(this bool state, Action trueAction)
    {
        if (state) trueAction();
        return state;
    }

    /// <summary>
    ///     Performs a <see cref="Action{T}"/> if the input boolean value is true.
    /// </summary>
    /// <typeparam name="T">The type to pass into the action.</typeparam>
    /// <param name="state">The boolean value that this extension method was called on.</param>
    /// <param name="trueAction">The action to perform, if <paramref name="state"/> equates to <c>true</c>.</param>
    /// <param name="args">The arguments to pass to the action.</param>
    /// <returns>The same boolean value as was passed into the method, as <paramref name="state"/></returns>
    public static bool ActIfTrue<T>(this bool state, Action<T> trueAction, T args)
    {
        if (state) trueAction(args);
        return state;
    }

    /// <summary>
    ///     Performs a <see cref="Action"/> if the input boolean value is false.
    /// </summary>
    /// <param name="state">The boolean value that this extension method was called on.</param>
    /// <param name="falseAction">The action to perform, if <paramref name="state"/> equates to <c>false</c>.</param>
    /// <returns>The same boolean value as was passed into the method, as <paramref name="state"/></returns>
    public static bool ActIfFalse(this bool state, Action falseAction)
    {
        if (!state) falseAction();
        return state;
    }

    /// <summary>
    ///     Performs a <see cref="Action{T}"/> if the input boolean value is false.
    /// </summary>
    /// <typeparam name="T">The type to pass into the action.</typeparam>
    /// <param name="state">The boolean value that this extension method was called on.</param>
    /// <param name="falseAction">The action to perform, if <paramref name="state"/> equates to <c>false</c>.</param>
    /// <param name="args">The arguments to pass to the action.</param>
    /// <returns>The same boolean value as was passed into the method, as <paramref name="state"/></returns>
    public static bool ActIfFalse<T>(this bool state, Action<T> falseAction, T args)
    {
        if (!state) falseAction(args);
        return state;
    }

    /// <summary>
    ///     Performs a <see cref="Action"/> if the input boolean value is true or false.
    /// </summary>
    /// <param name="state">The boolean value that this extension method was called on.</param>
    /// <param name="trueAction">The action to perform, if <paramref name="state"/> equates to <c>true</c>.</param>
    /// <param name="falseAction">The action to perform, if <paramref name="state"/> equates to <c>false</c>.</param>
    /// <returns>The same boolean value as was passed into the method, as <paramref name="state"/></returns>
    public static bool ActIf(this bool state, Action trueAction, Action falseAction)
    {
        if (state) trueAction();
        else falseAction();
        return state;
    }

    /// <summary>
    ///     Performs a <see cref="Action{T}"/> if the input boolean value is true.
    /// </summary>
    /// <typeparam name="T">The type to pass into the action.</typeparam>
    /// <param name="state">The boolean value that this extension method was called on.</param>
    /// <param name="trueAction">The action to perform, if <paramref name="state"/> equates to <c>true</c>.</param>
    /// <param name="falseAction">The action to perform, if <paramref name="state"/> equates to <c>false</c>.</param>
    /// <param name="args">The arguments to pass to the action.</param>
    /// <returns>The same boolean value as was passed into the method, as <paramref name="state"/></returns>
    public static bool ActIf<T>(this bool state, Action<T> trueAction, Action<T> falseAction, T args)
    {
        if (state) trueAction(args);
        else falseAction(args);
        return state;
    }
}