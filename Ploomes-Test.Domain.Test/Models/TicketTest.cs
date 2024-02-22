using FluentAssertions;
using Ploomes_Test.Domain.Exceptions;
using Ploomes_Test.Domain.Models;
using Xunit;

namespace Ploomes_Test.Domain.Test.Models;

public class TicketTest
{
        [Fact]
        public void Ticket_WithValidData_ShouldBeCreated()
        {
            // Arrange
            var requesterEmail = "requester@example.com";
            var subject = "Test Subject";
            var description = "Test Description";

            // Act
            var ticket = new Ticket(requesterEmail, subject, description);

            // Assert
            ticket.Should().NotBeNull();
            ticket.RequesterEmail.Should().Be(requesterEmail);
            ticket.Subject.Should().Be(subject);
            ticket.Description.Should().Be(description);
            ticket.Status.Should().Be(TicketStatus.Created);
        }

        [Theory]
        [InlineData("assignee@example.com", TicketStatus.InProgress)]
        [InlineData("assignee@example.com", TicketStatus.Created)]
        public void Assign_ValidAssigneeAndStatus_ShouldBeSuccessful(string assigneeEmail, TicketStatus initialStatus)
        {
            // Arrange
            var ticket = new Ticket("requester@example.com", "Test Subject", "Test Description");
            ticket.Status = initialStatus;

            // Act
            var result = ticket.Assign(assigneeEmail);

            // Assert
            result.IsSuccess.Should().BeTrue();
            ticket.AssigneeEmail.Should().Be(assigneeEmail);
            ticket.Status.Should().Be(TicketStatus.InProgress);
        }

        [Fact]
        public void Assign_InvalidStatus_ShouldFail()
        {
            // Arrange
            var ticket = new Ticket("requester@example.com", "Test Subject", "Test Description");
            ticket.Status = TicketStatus.Completed;

            // Act
            var result = ticket.Assign("assignee@example.com");

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle().Which.Should().BeOfType<ValidationError>();
        }

        [Fact]
        public void Complete_ValidStatus_ShouldBeSuccessful()
        {
            // Arrange
            var ticket = new Ticket("requester@example.com", "Test Subject", "Test Description");
            ticket.Status = TicketStatus.InProgress;

            // Act
            var result = ticket.Complete();

            // Assert
            result.IsSuccess.Should().BeTrue();
            ticket.Status.Should().Be(TicketStatus.Completed);
        }

        [Fact]
        public void Complete_InvalidStatus_ShouldFail()
        {
            // Arrange
            var ticket = new Ticket("requester@example.com", "Test Subject", "Test Description");
            ticket.Status = TicketStatus.Created;

            // Act
            var result = ticket.Complete();

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle().Which.Should().BeOfType<ValidationError>();
        }

        [Fact]
        public void Cancel_ValidStatus_ShouldBeSuccessful()
        {
            // Arrange
            var ticket = new Ticket("requester@example.com", "Test Subject", "Test Description");
            ticket.Status = TicketStatus.InProgress;

            // Act
            var result = ticket.Cancel("Test Reason");

            // Assert
            result.IsSuccess.Should().BeTrue();
            ticket.Status.Should().Be(TicketStatus.Cancelled);
        }

        [Theory]
        [InlineData(TicketStatus.Completed)]
        [InlineData(TicketStatus.Cancelled)]
        public void Cancel_InvalidStatus_ShouldFail(TicketStatus initialStatus)
        {
            // Arrange
            var ticket = new Ticket("requester@example.com", "Test Subject", "Test Description");
            ticket.Status = initialStatus;

            // Act
            var result = ticket.Cancel("Test Reason");

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle().Which.Should().BeOfType<ValidationError>();
        }
}
