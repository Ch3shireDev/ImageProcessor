namespace ImageProcessorLibrary.Services.Enums;

/// <summary>
///     Typ operacji binarnej.
/// </summary>
public enum BinaryOperationType
{
    /// <summary>
    ///     Operacja binarna AND.
    /// </summary>
    BINARY_AND,

    /// <summary>
    ///     Operacja binarna OR.
    /// </summary>
    BINARY_OR,

    /// <summary>
    ///     Operacja binarna XOR.
    /// </summary>
    BINARY_XOR,

    /// <summary>
    ///     Operacja binarna NOT
    /// </summary>
    BINARY_NOT,

    /// <summary>
    ///     Przekształcenie do maski 8-bitowej.
    /// </summary>
    TO_8BIT_MASK,

    /// <summary>
    ///     Przekształcenie do maski binarnej.
    /// </summary>
    TO_BINARY_MASK
}