using System;

namespace Blogposts.Common.Configuration.Options.Kafka
{
    /// <summary>
    /// Custom configuration settings for Kafka Consumers that are not part of the KAfka client's API.
    /// </summary>
    public class ConsumerSettings
    {
        /// <summary>
        /// Sets the semi-colon delimited string as a topic name source, e.g. "topic1;topic2".
        /// Consumers must call the <code>GetTopicSubscriptions()</code> method to get an array of topic names
        /// with which to configure their Kafka consumer.
        /// </summary>
        public string TopicSubscriptions { get; set; }

        /// <summary>
        /// Returns an array of topic names to which the consumer will subscribe. This is a required field.
        /// </summary>
        public string[] GetTopicSubscriptions() => string.IsNullOrWhiteSpace(TopicSubscriptions) ? new string[] { } : TopicSubscriptions.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
    }
}