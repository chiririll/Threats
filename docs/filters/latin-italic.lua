function Str(el)
  local result = pandoc.List()

  -- Split on hyphens and keep them
  for segment, sep in el.text:gmatch("([^-%s]+)([-%s]*)") do
    if segment:match("[A-Za-z]") then
      result:insert(pandoc.Emph{pandoc.Str(segment)})
    else
      result:insert(pandoc.Str(segment))
    end
    if sep ~= "" then
      result:insert(pandoc.Str(sep))
    end
  end

  return result
end
