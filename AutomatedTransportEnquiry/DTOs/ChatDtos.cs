namespace AutomatedTransportEnquiry.DTOs
{
    public class ChatMessageDto
    {
        public string Role { get; set; }      // "user" or "assistant"
        public string Content { get; set; }
    }

    public class ChatRequestDto
    {
        public string Message { get; set; }
        public List<ChatMessageDto> History { get; set; } = new();
    }

    public class ChatResponseDto
    {
        public string Reply { get; set; }
    }

}
