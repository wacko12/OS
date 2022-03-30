package com.example.demo2;

import java.util.Random;

public class Consumer implements Runnable {
    private volatile SimpleDataQueue queue;
    private volatile boolean ready = false;

    public Consumer(SimpleDataQueue queue) {
        this.queue = queue;
    }

    public void run() {
        for(Random rand = new Random(); !this.ready || this.queue.getElementsCount() != 0; System.out.println("Queue elements size is: " + this.queue.getElementsCount())) {
            try {
                Thread.sleep((long)rand.nextInt(1000));
            } catch (InterruptedException var3) {
                var3.printStackTrace();
            }

            if (this.queue.getElementsCount() > 0) {
                this.queue.remove();
            }
        }

        System.out.println("                    Ending " + Thread.currentThread().getName());
    }

    public void shutdown() {
        this.ready = true;
    }
}