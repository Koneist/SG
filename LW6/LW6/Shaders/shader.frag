float convertRgb(float value) {
    return value / 256.0;
}

void main() {
    gl_FragColor = vec4(convertRgb(14.0),convertRgb(209.0),convertRgb(20.0),1.0);
}
