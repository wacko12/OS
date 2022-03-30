package com.example.demo2;

public class SimpleDataQueue {
    private int head;
    private int tail;
    private volatile int elementsCount;
    private Integer[] myArrayQueue;

    public SimpleDataQueue(int size) {
        this.myArrayQueue = new Integer[size];
    }

    public void add(Integer element) {
        synchronized(this) {
            while(this.elementsCount >= 100) {
                try {
                    this.wait();
                } catch (InterruptedException var5) {
                    var5.printStackTrace();
                }
            }

            this.myArrayQueue[this.head] = element;
            ++this.elementsCount;
            if (this.head == this.myArrayQueue.length - 1) {
                this.head = 0;
            } else {
                ++this.head;
            }

            this.notifyAll();
        }
    }

    public Integer remove() {
        synchronized(this) {
            while(this.getElementsCount() == 0) {
                try {
                    this.wait();
                } catch (InterruptedException var4) {
                    var4.printStackTrace();
                }
            }

            Integer value = this.myArrayQueue[this.tail];
            this.myArrayQueue[this.tail] = null;
            --this.elementsCount;
            if (this.tail == this.myArrayQueue.length - 1) {
                this.tail = 0;
            } else {
                ++this.tail;
            }

            if (this.elementsCount <= 80) {
                this.notifyAll();
            }

            return value;
        }
    }

    public synchronized int getElementsCount() {
        return this.elementsCount;
    }
}