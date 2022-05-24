uniform float uTime;

void setPosition(vec4 position) {
    gl_Position = gl_ProjectionMatrix * gl_ModelViewMatrix * position;
}

float square(float value) {
    return value * value;
}

float calcStartZ(float p, float q) {
    return (square(gl_Vertex.x) / p + square(gl_Vertex.y) / q) / 2.0;
}

float calcEndZ(float p, float q) {
    return ((square(gl_Vertex.x) / (2.0 * p)) - (square(gl_Vertex.y) / (2.0 * q)));
}

float calculateCurrentZ(float time, float timeToMorph, float startZ, float endZ) {
    float delta;
    if (startZ > endZ) {
        delta = startZ - endZ;
        return startZ - (time * delta) / timeToMorph;
    }
    else {
        delta = endZ - startZ;
        return startZ + (time * delta) / timeToMorph;
    }
}

void main() {
    float timeToMorph = 5000.0;
    float time = uTime;
    if (time > timeToMorph) {
        time = timeToMorph;
    }

    float p = 4.0;
    float q = 3.0;

    float startZ = calcStartZ(p, q);
    float endZ = calcEndZ(p, q);

    float currZ = calculateCurrentZ(time, timeToMorph, startZ, endZ);

    setPosition(vec4( gl_Vertex.x, gl_Vertex.y, gl_Vertex.z, 1.0));
}
