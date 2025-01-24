class Main inherits IO {
    is_prime(n : Int, math : Math) : SELF_TYPE {
        {
            if n < 2 then false else {
                j <- 2;
                isPrime <- true;

                while j < n loop {
                    if math.modulo(n, j) = 0 then {
                        isPrime <- false;
                    } else { isPrime <- true; } fi;
                    j <- j + 1;
                } pool;

                if isPrime then out_intln(n) else { self; } fi;

                self;
            } fi;
        }
    };

    main() : SELF_TYPE {
        {
            math <- new Math;
            out_string("Enter an upper limit: ");
            countUntil <- in_int();
            out_string("Checking prime numbers up to ");
            out_intln(countUntil);

            i <- 2;
            while i <= countUntil loop {
                is_prime(i, math);
                i <- i + 1;
            } pool;

            self;
        }
    };
};

class Math {
    modulo(a : Int, b : Int) : Int {
        {
            if b = 0 then {
                out_stringln("Error: Division by zero in modulo operation.");
                0;
            } else {
                a - (a / b) * b;
            } fi;
        }
    };
};
