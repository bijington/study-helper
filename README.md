# Study Helper

A .NET MAUI example application demonstrating how to embed AI services within a cross-platform mobile and desktop application.

## Overview

Study Helper showcases best practices for integrating modern AI services into .NET MAUI applications. This project includes practical examples of how to leverage both OpenAI and Azure OpenAI services to build intelligent features across iOS, Android, macOS, and Windows platforms.

## Features

### Chat

The Chat feature demonstrates integration with OpenAI services. This module shows how to implement conversational AI capabilities in your application.

[Learn more about the Chat implementation →](./StudyHelper/Chat/README.md)

### Vision

The Vision feature demonstrates integration with Azure OpenAI services, including computer vision capabilities for analyzing images and visual content.

[Learn more about the Vision implementation →](./StudyHelper/Vision/README.md)

## Getting Started

1. Clone this repository
2. Open `StudyHelper.slnx` in your IDE (Visual Studio, Rider, or VS Code)
3. Configure your API keys for OpenAI and Azure OpenAI services
4. Build and run the application on your target platform

## Project Structure

- **Chat/** - OpenAI integration examples
- **Vision/** - Azure OpenAI integration examples
- **Models/** - Shared data models used across features
- **Platforms/** - Platform-specific code for iOS, Android, macOS, and Windows

## Requirements

- .NET 10.0 or later
- API keys for OpenAI and/or Azure OpenAI services

## License

See the [LICENSE](./LICENSE) file for details.

## Inspiration

Inspiration for elements such as the Vision implementation came from the fantastic https://github.com/davidortinau/BaristaNotes built by @DavidOrtinau.