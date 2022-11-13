namespace ImageProcessorLibrary.Services;

public interface IBorderService
{
    void FillBorderConstant();

    void FillResultBorderConstant();

    void FillBorderReflect();

    void FillBorderWrap();
}