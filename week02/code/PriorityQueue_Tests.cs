using Microsoft.VisualStudio.TestTools.UnitTesting;

// // TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add multiple items with different priorities and verify highest priority is dequeued first
    // Expected Result: Items should be dequeued in order of priority (highest first)
    // Defect(s) Found: The Dequeue method doesn't remove the item from the queue after retrieving it
    public void TestPriorityQueue_DifferentPriorities()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("Low", 1);
        queue.Enqueue("Medium", 2);
        queue.Enqueue("High", 3);

        Assert.AreEqual("High", queue.Dequeue(), "Should dequeue highest priority (3) first");
        Assert.AreEqual("Medium", queue.Dequeue(), "Should dequeue next highest priority (2) second");
        Assert.AreEqual("Low", queue.Dequeue(), "Should dequeue lowest priority (1) last");
    }

    [TestMethod]
    // Scenario: Add multiple items with the same priority and verify FIFO order
    // Expected Result: Items with same priority should be dequeued in the order they were added
    // Defect(s) Found: The Dequeue method is using >= which causes it to take the later item instead of 
    // following FIFO order for same priority items
    public void TestPriorityQueue_SamePriority()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("First", 1);
        queue.Enqueue("Second", 1);
        queue.Enqueue("Third", 1);

        Assert.AreEqual("First", queue.Dequeue(), "Should dequeue first item added with priority 1");
        Assert.AreEqual("Second", queue.Dequeue(), "Should dequeue second item added with priority 1");
        Assert.AreEqual("Third", queue.Dequeue(), "Should dequeue third item added with priority 1");
    }

    [TestMethod]
    // Scenario: Mix of same and different priorities
    // Expected Result: Higher priorities should be dequeued first, same priorities in FIFO order
    // Defect(s) Found: Both issues above affect this test - items aren't removed and FIFO order is not maintained
    public void TestPriorityQueue_MixedPriorities()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("First-P1", 1);
        queue.Enqueue("Second-P1", 1);
        queue.Enqueue("First-P2", 2);
        queue.Enqueue("Third-P1", 1);
        queue.Enqueue("Second-P2", 2);

        Assert.AreEqual("First-P2", queue.Dequeue(), "Should dequeue first priority 2 item");
        Assert.AreEqual("Second-P2", queue.Dequeue(), "Should dequeue second priority 2 item");
        Assert.AreEqual("First-P1", queue.Dequeue(), "Should dequeue first priority 1 item");
        Assert.AreEqual("Second-P1", queue.Dequeue(), "Should dequeue second priority 1 item");
        Assert.AreEqual("Third-P1", queue.Dequeue(), "Should dequeue third priority 1 item");
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from empty queue
    // Expected Result: Should throw InvalidOperationException
    // Defect(s) Found: None - empty queue check works correctly
    public void TestPriorityQueue_EmptyQueue()
    {
        var queue = new PriorityQueue();

        try
        {
            queue.Dequeue();
            Assert.Fail("Should have thrown exception for empty queue");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Verify items are added to back of queue
    // Expected Result: New items should be added to back regardless of priority
    // Defect(s) Found: None - items are correctly added to back of queue
    public void TestPriorityQueue_EnqueueToBack()
    {
        var queue = new PriorityQueue();
        queue.Enqueue("First", 1);
        queue.Enqueue("Second", 2);
        
        Assert.AreEqual("[First (Pri:1), Second (Pri:2)]", queue.ToString(), 
            "Items should be in order of addition");
    }

    // Add more test cases as needed below.
}