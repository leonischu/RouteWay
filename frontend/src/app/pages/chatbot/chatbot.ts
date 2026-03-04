import { Component, ElementRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Api } from '../../services/api';

export interface ChatMessage {
  role: 'user' | 'assistant';
  content: string;
  timestamp: Date;
}

@Component({
  selector: 'app-chatbot',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './chatbot.html',
  styleUrl: './chatbot.css'
})
export class ChatbotComponent {
  @ViewChild('messagesBox') messagesBox!: ElementRef;

  isOpen = false;
  isTyping = false;
  userInput = '';

  messages: ChatMessage[] = [
    {
      role: 'assistant',
      content: '👋 Hi! I\'m your transport assistant. Ask me about schedules or your bookings!',
      timestamp: new Date()
    }
  ];

  constructor(private api: Api) {}

  toggleChat(): void {
    this.isOpen = !this.isOpen;
    if (this.isOpen) setTimeout(() => this.scrollToBottom(), 100);
  }

  sendMessage(): void {
    const text = this.userInput.trim();
    if (!text || this.isTyping) return;

    this.messages.push({ role: 'user', content: text, timestamp: new Date() });
    this.userInput = '';
    this.isTyping = true;
    this.scrollToBottom();

    const history = this.messages.map(m => ({ role: m.role, content: m.content }));

    this.api.sendChatMessage(text, history).subscribe({
      next: (res) => {
        this.isTyping = false;
        this.messages.push({
          role: 'assistant',
          content: res.status ? res.data.reply : '⚠️ Sorry, something went wrong.',
          timestamp: new Date()
        });
        this.scrollToBottom();
      },
      error: () => {
        this.isTyping = false;
        this.messages.push({
          role: 'assistant',
          content: '⚠️ Connection error. Please try again.',
          timestamp: new Date()
        });
        this.scrollToBottom();
      }
    });
  }

  quickAsk(question: string): void {
    this.userInput = question;
    this.sendMessage();
  }

  onEnter(event: KeyboardEvent): void {
    if (event.key === 'Enter' && !event.shiftKey) {
      event.preventDefault();
      this.sendMessage();
    }
  }

  clearChat(): void {
    this.messages = [this.messages[0]];
  }

  private scrollToBottom(): void {
    setTimeout(() => {
      const el = this.messagesBox?.nativeElement;
      if (el) el.scrollTop = el.scrollHeight;
    }, 60);
  }
}