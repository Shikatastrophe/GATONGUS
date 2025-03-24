class Rectangle {
    constructor(height, width) {
        this.height = height;
        this.width = width;
    }
    // Getter
    get area() {
        return this.calcArea();
    }
    // Method
    calcArea() {
        return this.height * this.width;
    }
    *getSides() {
        yield this.height;
        yield this.width;
        yield this.height;
        yield this.width;
    }
    get perimeter() {
        return this.calcPeri();
    }
    get pera2() {
        return this.calcPeri2();
    }
    calcPeri() {
        let peri = [...this.getSides()];
        let perifinal = 0;
        for (let i = 0; i < peri.length; i++) {
            perifinal += peri[i];
        }
        return perifinal;
    }
    calcPeri2() {
        let perifinalfinal = this.height * 2 + this.width * 2;
        return perifinalfinal;
    }
}

const square = new Rectangle(10, 10);  
console.log("Area: " + square.area); // 100
console.log("Lados: " + [...square.getSides()]); // [10, 10, 10, 10]
console.log("Perimetro: " + square.perimeter);
console.log("Perimetro final: " + square.pera2); 
