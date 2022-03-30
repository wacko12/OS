package com.example.demo2;

import java.util.Random;

public class Producer implements Runnable {
    private SimpleDataQueue queue;
    private volatile boolean ready = false;

    public Producer(SimpleDataQueue queue) {
        this.queue = queue;
    }

    public void run() {
        Random rand = new Random();

        while(!this.ready) {
            try {
                Thread.sleep((long)rand.nextInt(1000));
            } catch (InterruptedException var3) {
                var3.printStackTrace();
            }

            this.queue.add(rand.nextInt(100));
            System.out.println("Queue elements size is: " + this.queue.getElementsCount());
        }

        System.out.println("                    Ending " + Thread.currentThread().getName());
    }

    public void shutdown() {
        this.ready = true;
    }
}