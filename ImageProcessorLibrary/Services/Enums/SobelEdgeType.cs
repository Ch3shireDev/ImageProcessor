namespace ImageProcessorLibrary.Services.Enums;

/// <summary>
///     Typ operacji krawędziowania Sobela.
/// </summary>
public enum SobelEdgeType
{
    /// <summary>
    ///    Operacja krawędziowania Sobela dla wszystkich kierunków.
    /// </summary>
    ALL,
    /// <summary>
    ///   Operacja krawędziowania Sobela dla kierunku wschodniego.
    /// </summary>
    EAST,
    /// <summary>
    ///  Operacja krawędziowania Sobela dla kierunku północno-wschodniego.
    /// </summary>
    NORTH_EAST,
    /// <summary>
    /// Operacja krawędziowania Sobela dla kierunku północnego.
    /// </summary>
    NORTH,
    /// <summary>
    /// Operacja krawędziowania Sobela dla kierunku północno-zachodniego.
    /// </summary>
    NORTH_WEST,
    /// <summary>
    /// Operacja krawędziowania Sobela dla kierunku zachodniego.
    /// </summary>
    WEST,
    /// <summary>
    /// Operacja krawędziowania Sobela dla kierunku południowo-wschodniego.
    /// </summary>
    SOUTH_WEST,
    /// <summary>
    /// Operacja krawędziowania Sobela dla kierunku południowego.
    /// </summary>
    SOUTH,
    /// <summary>
    /// Operacja krawędziowania Sobela dla kierunku południowo-wschodniego.
    /// </summary>
    SOUTH_EAST
}