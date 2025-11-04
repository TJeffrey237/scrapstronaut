shader_type canvas_item;

uniform float wave_intensity : hint_range(0.0, 0.2) = 0.1;
uniform float wave_frequency : hint_range(5.0, 50.0) = 10.0;
uniform float wave_speed : hint_range(0.1, 5.0) = 2.0;

uniform vec4 color_start : source_color = vec4(1.0, 0.5, 0.0, 1.0);
uniform vec4 color_end : source_color = vec4(1.0, 0.0, 0.5, 1.0);

void fragment() {
    vec2 uv = UV;
    
    uv.x += sin(uv.y * wave_frequency + TIME * wave_speed) * wave_intensity;
    
    vec4 distorted_color = texture(TEXTURE, uv);
    vec4 gradient_color = mix(color_start, color_end, uv.y); 
    vec4 final_color = gradient_color;
    
    final_color.a *= distorted_color.a; 
    final_color.a *= COLOR.a; 
    COLOR = final_color;
}