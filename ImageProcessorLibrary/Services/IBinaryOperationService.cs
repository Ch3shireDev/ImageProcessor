namespace ImageProcessorLibrary.Services;

public interface IBinaryOperationService
{
    void BinaryAnd();

    void BinaryOr();

    void BinaryXor();

    void BinaryNot();

    void GetBinaryMask();

    void Get8BitMask();
}