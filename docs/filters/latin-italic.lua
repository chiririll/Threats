function is_latin_or_greek_letter(c)
  local byte = string.byte(c)
  -- Latin uppercase: A-Z (65–90), lowercase: a-z (97–122)
  if (byte >= 65 and byte <= 90) or (byte >= 97 and byte <= 122) then
    return true
  end

  -- UTF-8 Greek letters start with multi-byte characters; handle them properly
  local codepoint = utf8.codepoint(c)
  -- Greek uppercase: Α (U+0391) to Ω (U+03A9), lowercase: α (U+03B1) to ω (U+03C9)
  if (codepoint >= 0x391 and codepoint <= 0x3A9) or (codepoint >= 0x3B1 and codepoint <= 0x3C9) then
    return true
  end

  return false
end

function split_into_latin_and_nonlatin(str)
  local result = {}
  local current = ""
  local in_latin = nil
  local i = 1

  while i <= #str do
    local cp = utf8.codepoint(str, i)
    local c = utf8.char(cp)
    local latin = is_latin_or_greek_letter(c)

    if in_latin == nil then
      current = c
      in_latin = latin
    elseif latin == in_latin then
      current = current .. c
    else
      table.insert(result, {text = current, italic = in_latin})
      current = c
      in_latin = latin
    end

    i = i + #c
  end

  if current ~= "" then
    table.insert(result, {text = current, italic = in_latin})
  end

  return result
end

function Str(el)
  local pieces = split_into_latin_and_nonlatin(el.text)
  if #pieces == 1 and not pieces[1].italic then
    return nil -- keep as-is
  end

  local result = {}
  for _, part in ipairs(pieces) do
    if part.italic then
      table.insert(result, pandoc.Emph{pandoc.Str(part.text)})
    else
      table.insert(result, pandoc.Str(part.text))
    end
  end
  return result
end
