uniform float uTime;

void setPosition(vec4 position) 
{
    gl_Position = gl_ProjectionMatrix * gl_ModelViewMatrix * position;
}

float square(float value) 
{
    return value * value;
}

float GetStartY(float a, float b) {
    return (square(gl_Vertex.x) / a + square(gl_Vertex.z) / b) / 2.0;
}

float GetEndY(float a, float b) 
{
    return ((square(gl_Vertex.x) / (2.0 * a)) - (square(gl_Vertex.z) / (2.0 * b)));
}

float GetCurrentY(float time, float startY, float endY) 
{
    return mix(startY, endY, time);
}

void main() 
{

    float a = 1.0;
    float b = 1.0;

    float startY = GetStartY(a, b);
    float endY = GetEndY(a, b);

    float currY = GetCurrentY(sin(uTime), startY, endY);

    setPosition(vec4( gl_Vertex.x, currY, gl_Vertex.z, 1.0));
}
