namespace ImageProcessorLibrary.Services;

public interface IEdgeService
{
    void PrewittEdgeDetection();

    void CannyOperatorEdgeDetection();
    void SobelEdgeDetection();

    void EdgeSobelEast();

    void EdgeSobelNorthEast();

    void EdgeSobelNorth();

    void EdgeSobelNorthWest();

    void EdgeSobelWest();

    void EdgeSobelSouthWest();

    void EdgeSobelSouth();

    void EdgeSobelSouthEast();

}