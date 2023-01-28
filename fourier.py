import cv2
import numpy as np
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D

# Read image file
img = cv2.imread('lion.jpg', cv2.IMREAD_GRAYSCALE)

# Perform Fourier transform
fourier = np.fft.fft2(img)

# Shift the zero-frequency component to the center of the spectrum
fourier_shift = np.fft.fftshift(fourier)

# Compute the magnitude of the transformed image
magnitude = 20*np.abs(fourier_shift)

# Plot the magnitude
# plt.imshow(magnitude, cmap='gray')
# plt.show()

# Create a figure and 3D axes
fig = plt.figure()
ax = fig.add_subplot(111, projection='3d')

# Plot the magnitude as a 3D surface
x, y = np.linspace(0, magnitude.shape[1], magnitude.shape[1]), np.linspace(0, magnitude.shape[0], magnitude.shape[0])
X, Y = np.meshgrid(x, y)
ax.plot_surface(X, Y, magnitude)
plt.show()