package com.example.demo2;
import java.io.IOException;

public class Main {
    public Main() {
    }

    public static void main(String[] args) throws InterruptedException, IOException {
        SimpleDataQueue queue = new SimpleDataQueue(200);
        Producer producer = new Producer(queue);
        Consumer consumer = new Consumer(queue);
        Thread t1 = new Thread(producer);
        Thread t2 = new Thread(producer);
        Thread t3 = new Thread(producer);
        Thread t4 = new Thread(consumer);
        Thread t5 = new Thread(consumer);
        t1.start();
        t2.start();
        t3.start();
        t4.start();
        t5.start();
        int code = System.in.read();
        char ch = (char)code;
        System.out.println("you pressed: '" + ch + "'\n");
        if ('q' == ch) {
            producer.shutdown();
        }

        t1.join();
        t2.join();
        t3.join();
        consumer.shutdown();
        t4.join();
        t5.join();
        System.out.println("Finish!");
    }
}