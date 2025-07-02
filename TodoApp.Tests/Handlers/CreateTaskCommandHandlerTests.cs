using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Moq;
using TodoApp.Application.Commands.CreateTask;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Tests.Handlers;

public class CreateTaskCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_ReturnsGuid()
    {
        // Arrange
        var mockRepo = new Mock<ITaskRepository>();
        mockRepo.Setup(repo => repo.AddAsync(It.IsAny<TaskItem>()))
                .Returns(Task.CompletedTask);

        var handler = new CreateTaskCommandHandler(mockRepo.Object);

        var command = new CreateTaskCommand(
            Title: "Test Task",
            Description: "A unit test case",
            DueDate: DateTime.Now.AddDays(1)
        );

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.IsType<Guid>(result);
        Assert.NotEqual(Guid.Empty, result);

        mockRepo.Verify(repo => repo.AddAsync(It.IsAny<TaskItem>()), Times.Once);
    }
}
