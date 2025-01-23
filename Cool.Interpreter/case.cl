class Main {
    main() : Object {
        let input : Object <- (new Int).init(42) in
        case input of
            i : Int => out_int(i + 10);
            s : String => out_string("String gefunden: " ^ s);
        esac;
    };
};