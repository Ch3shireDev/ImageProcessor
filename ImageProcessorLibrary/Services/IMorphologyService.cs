namespace ImageProcessorLibrary.Services;

public interface IMorphologyService
{
    void MorphologyErosion();

    void MorphologyDilation();

    void MorphologyOpening();

    void MorphologyClosing();
}