import java.util.Scanner;
public class Main
{
	public static void main(String[] args) {
	    float result, sum = 0, i, b, c;
		        Scanner in = new Scanner(System.in);
        System.out.print("Введите i, b, c, искомой функции: \n");
        i = in.nextInt();
        System.out.printf("i: %f \n", i);
        b = in.nextInt();
        System.out.printf("b: %f \n", b);
        c = in.nextInt();
        System.out.printf("c: %f \n", c);
        long start = System.currentTimeMillis();
        int n2 = 100000000;
            for (int n1 = 0; n1 < n2; n1++)
            {
                sum = b * 2 + c - i;
            }
        result = i + sum;
        System.out.printf("f(i+1) %f \n", result);
        in.close();
        long finish = System.currentTimeMillis();
        long elapsed = finish - start;
        System.out.println("Прошло времени, мс: " + elapsed);
	}
}